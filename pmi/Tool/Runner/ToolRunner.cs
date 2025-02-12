using pmi.Tool.Models;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace pmi.Tool.Runner;

public class ToolRunner
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

    public async Task ExecuteToolAsync(ToolExecutionRequest toolExecution, WebSocket webSocket)
    {
        string output = string.Empty;
        Process process = new Process();
        process.StartInfo.FileName = toolExecution.Tool;           // Command to run
        process.StartInfo.Arguments = $"{toolExecution.Target}  {toolExecution.Arguments}";     // Arguments for the command

        // Configure the process to redirect output, so we can capture it
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        try
        {

            process.Start();

            // Read and send output line by line
            while (!process.StandardOutput.EndOfStream)
            {
                string? line = await process.StandardOutput.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    Console.WriteLine(line);
                    byte[] messageBytes = Encoding.UTF8.GetBytes(line);
                    await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }

            // Read and send errors if any
            while (!process.StandardError.EndOfStream)
            {
                string? errorLine = await process.StandardError.ReadLineAsync();
                if (!string.IsNullOrEmpty(errorLine))
                {
                    Console.WriteLine($"Error: {errorLine}");
                    byte[] errorBytes = Encoding.UTF8.GetBytes($"Error: {errorLine}");
                    await webSocket.SendAsync(new ArraySegment<byte>(errorBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }

            process.WaitForExit();
            await webSocket.SendAsync(Encoding.UTF8.GetBytes("Process completed."), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        catch (Exception ex)
        {
            byte[] errorBytes = Encoding.UTF8.GetBytes($"Exception: {ex.Message}");
            await webSocket.SendAsync(new ArraySegment<byte>(errorBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

}