using pmi.Project.Models;

namespace pmi.Project.Builders;

public class ProjectEntityBuilder
{
    public ProjectEntity createNewProjectEntity(CreateProjectDto p)
    {

        return new ProjectEntity
        {
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