namespace pmi.Project.Models;

public record ProjectDto(string Name, DateTime CreatedDate, DateTime? LastUpdated, ProjectInfo ProjectInfo);
