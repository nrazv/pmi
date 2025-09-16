using pmi.ExecutedTool.Models;

namespace pmi.Tool.Services
{
    public interface IToolService
    {
        public Task<List<ExecutedToolEntity>> GetExecutedToolsByProjectName(string projectName);

    }
}
