using pmi.Tool.Models;
using System.Diagnostics;

namespace pmi.Tool.Runner;

public class ToolRunner
{
    public void RunTool(ToolExecutionRequest toolExecution)
    {
        if (toolExecution is null)
        {
            return;
        }

        Run(toolExecution);
    }

    private void Run(ToolExecutionRequest toolExecution)
    {
        // Define the command to run nmap through WSL
        string command = "wsl nmap"; // Example nmap command

        // Create a new process to run the command
        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c {command}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Start the process and capture the output
        using (Process process = Process.Start(processInfo))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
    }
}
