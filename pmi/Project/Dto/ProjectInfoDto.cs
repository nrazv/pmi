namespace pmi.Project.Models;

public record ProjectInfoDto(string Name, DateTime CreatedDate, DateTime? LastUpdated, string Status);