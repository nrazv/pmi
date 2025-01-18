using System.ComponentModel.DataAnnotations;

namespace pmi.Project.Models;

public class ProjectInfo
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; init; } = null!;

    public DateTime CreatedDate { get; init; }

    public DateTime? LastUpdated { get; init; }

    [Required]
    public ProjectStatus Status { get; set; }


    public ProjectInfo() { }
    public ProjectInfo(string id, string name, DateTime createdDate, DateTime? lastUpdated, ProjectStatus status)
    {
        Id = id;
        Name = name;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        Status = status;
    }


}