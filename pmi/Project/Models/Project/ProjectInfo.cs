namespace pmi.Project.Models;

public class ProjectInfo
{
    public string ProjectName { get; init; } = null!;
    public DateTime ProjectCreatedDate { get; init; }
    public DateTime? ProjectLastUpdated { get; init; }
    public ProjectStatus ProjectStatus { get; set; }

}