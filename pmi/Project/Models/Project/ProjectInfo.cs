using System.ComponentModel.DataAnnotations;

namespace pmi.Project.Models;

public class ProjectInfo
{
    [Key]
    public string Id { get; init; } = null!;
    public string ProjectName { get; init; } = null!;
    public DateTime CreatedDate { get; init; }
    public DateTime? LastUpdated { get; init; }
    public ProjectStatus Status { get; set; }

    public ProjectInfo(string id, string projectName, DateTime createdDate, DateTime? lastUpdated, ProjectStatus status)
    {
        Id = id;
        ProjectName = projectName;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        Status = status;
    }
}