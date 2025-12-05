using pmi.ExecutedTool.Models;
using pmi.Project.Models;
using pmi.Tool.Models;

namespace pmi.Utilities;

public static class ProjectFactory
{

    public static ExecutedToolEntity CreateExecutedToolFromExecutionRequest(ToolExecutionRequest request, ProjectEntity project, Guid toolId, string? runnerId)
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

    public static void PatchProject(ProjectEntity project, PatchProjectDto patchProject)
    {

        if (patchProject.Status.HasValue)
            project.ProjectInfo.Status = patchProject.Status.Value;

        if (!string.IsNullOrEmpty(patchProject.IpAddress))
            project.IpAddress = patchProject.IpAddress;

        if (!string.IsNullOrEmpty(patchProject.Description))
            project.Description = patchProject.Description;

        if (!string.IsNullOrEmpty(patchProject.DomainName))
            project.DomainName = patchProject.DomainName;
    }
}