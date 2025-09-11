using pmi.Tool.Models;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace pmi.Tool.Managers;

public class ProcessManager
{



    public string RunTool(ToolExecutionRequest toolExecution)
    {
        if (toolExecution is null)
        {
            return string.Empty;
        }

        var response = runTool(toolExecution);

        return response == null ? string.Empty : response;
    }

    private string? runTool(ToolExecutionRequest toolExecution)
    {
        string output = string.Empty;
        Process process = new Process();
        process.StartInfo.FileName = toolExecution.Tool;           // Command to run
        process.StartInfo.Arguments = $"{toolExecution.Target}  {toolExecution.Arguments}";     // Arguments for the command

        // Configure the process to redirect output, so we can capture it
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;       // Optional: prevents a new window (more relevant for GUI apps)

        try
        {
            // Start the process
            process.Start();

            // Capture output and error messages
            output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            // Wait for the process to finish
            process.WaitForExit();

            // Display the output
            Console.WriteLine(output);
            // Display any errors (in case nmap isn't found or there's an issue)
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Error:");
                Console.WriteLine(error);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        return output;
    }


    public Process CreateNewProcess(ToolExecutionRequest toolExecution)
    {
        Process process = new Process
        {
            StartInfo = {
            FileName = toolExecution.Tool,
                Arguments = $"{toolExecution.Target}  {toolExecution.Arguments}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            },
            EnableRaisingEvents = true
        };

        return process;
    }

    public async Task HandleOutputAsync(DataReceivedEventArgs e, WebSocket webSocket)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            Console.WriteLine(e.Data);
            byte[] messageBytes = Encoding.UTF8.GetBytes(e.Data);
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }


    public async Task HandleErrorDataReceived(DataReceivedEventArgs e, WebSocket webSocket)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            Console.WriteLine($"Error: {e.Data}");
            byte[] errorBytes = Encoding.UTF8.GetBytes($"Error: {e.Data}");
            await webSocket.SendAsync(new ArraySegment<byte>(errorBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    public Task HandleProcessExited(Process process, ToolExecutionRequest request)
    {
        WriteLine($"Process with id {process.Id} has exited.");
        WriteLine($"Closing {nameof(WebSocket)} connection");
        return Task.CompletedTask;
    }

}