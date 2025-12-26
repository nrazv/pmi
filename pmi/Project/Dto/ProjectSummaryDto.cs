namespace pmi.Project.Models;

public record ProjectSummaryDto(Guid Id, string Name, string? Description, string? DomainName, string? IpAddress, ProjectInfoDto ProjectInfo);