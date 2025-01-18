namespace pmi.Project.Models;

public record ProjectInfoDto(string Id, string Name, DateTime CreatedDate, DateTime? LastUpdated, string Status);