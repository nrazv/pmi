using pmi.Tool.Models;
using System.Diagnostics;

namespace pmi.Tool.Managers;

public class ProcessManager
{
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
}