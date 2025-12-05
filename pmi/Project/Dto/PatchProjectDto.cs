
namespace pmi.Project.Models;

public record PatchProjectDto(string? Description, string? DomainName, string? IpAddress, DateTime? LastUpdated, ProjectStatus? Status);