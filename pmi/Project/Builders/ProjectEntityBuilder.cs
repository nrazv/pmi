using pmi.Project.Models;

namespace pmi.Project.Builders;

public class ProjectEntityBuilder
{
    public ProjectEntity createNewProjectEntity(CreateProjectDto p)
    {
        var projectId = Guid.NewGuid().ToString();

        return new ProjectEntity
        {
            Id = projectId,
            Name = p.Name,
            DomainName = p.DomainName ?? string.Empty,
            IpAddress = p.IpAddress ?? string.Empty,
            ProjectInfo = new ProjectInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = p.Name,
                CreatedDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Status = ProjectStatus.NotStarted
            }
        };
    }
}