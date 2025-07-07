using pmi.Project.Models;
using pmi.Tool.Models;

namespace pmi.Utilities;

public static class ProjectFactory
{

    public static ExecutedToolEntity CreateExecutedToolFromExecutionRequest(ToolExecutionRequest request, ProjectEntity project, string toolId, string? runnerId)
    {
        return new ExecutedToolEntity
        {
            Id = toolId,
            ProjectId = project.Id,
            Project = project,
            Name = request.Tool,
            RunnerId = runnerId,
            Status = ExecutionStatus.Running,
            Target = request.Target,
            ClientId = request.ClientId,
            ToolArguments = request.Arguments,
            ExecutedDated = DateTime.Now
        };
    }
}