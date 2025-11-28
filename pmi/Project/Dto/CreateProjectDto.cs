namespace pmi.Project.Models;

public record CreateProjectDto(string Name, string? Description, string? DomainName, string? IpAddress);