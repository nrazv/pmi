using pmi.Tool.Models;

namespace pmi.Tool.Services
{
    public interface IToolService
    {
        public string RunTool(ToolExecutionRequest toolExecution);
    }
}
