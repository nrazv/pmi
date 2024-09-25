using pmi.Tool.Models;

namespace pmi.Tool.Services
{
    public interface IToolService
    {
        public void RunTool(ToolExecutionRequest toolExecution);
    }
}
