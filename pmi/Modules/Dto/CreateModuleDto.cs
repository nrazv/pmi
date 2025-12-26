using pmi.Tool.Models;

namespace pmi.Modules.Dto;

public record CreateModuleDto(string Name, string? Description, List<ExecutableToolDto> ExecutableTools);