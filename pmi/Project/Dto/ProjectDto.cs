using pmi.ExecutedTool.Models;
using pmi.Modules.Models;
using pmi.Subdomain.Models;

namespace pmi.Project.Models;

public record ProjectDto(Guid Id, string Name, string? Description, string? DomainName, string? IpAddress,
ProjectInfoDto ProjectInfo, ICollection<ExecutedToolEntity> ExecutedTools,
ICollection<ExecutedModuleEntity> ExecutedModules,
List<SubdomainEntity> Subdomains
);