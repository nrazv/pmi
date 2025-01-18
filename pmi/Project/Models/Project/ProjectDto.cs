namespace pmi.Project.Models;

public record ProjectDto(string Name, string? DomainName, string? IpAddress, ProjectInfoDto ProjectInfo);