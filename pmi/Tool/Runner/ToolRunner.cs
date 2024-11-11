using pmi.Tool.Models;
using System.Diagnostics;

namespace pmi.Tool.Runner;

public class ToolRunner
{
    public string RunTool(ToolExecutionRequest toolExecution)
    {
        if (toolExecution is null)
        {
            return string.Empty;
        }

        return runTool(toolExecution);
    }

    private string? runTool(ToolExecutionRequest toolExecution)
    {
        string output = string.Empty;
        // Set up the process to run the nmap command
        Process process = new Process();
        process.StartInfo.FileName = "nmap";           // Command to run
        process.StartInfo.Arguments = "--version";     // Arguments for the command

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
            Console.WriteLine("Nmap Version Information:");
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

}