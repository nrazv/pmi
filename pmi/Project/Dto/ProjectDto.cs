using pmi.ExecutedTool.Models;

namespace pmi.Project.Models;

public record ProjectDto(Guid Id, string Name, string? DomainName, string? IpAddress, ProjectInfoDto ProjectInfo, ICollection<ExecutedToolEntity> ExecutedTools);