using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using pmi.ExecutedTool.Models;
using pmi.Modules.Models;

namespace pmi.Project.Models;


[Index(nameof(Name), IsUnique = true)]
public class ProjectEntity
{
    [Key]
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; set; }
    public string? DomainName { get; set; }
    public string? IpAddress { get; set; }
    public ProjectInfo ProjectInfo { get; set; } = null!;

    public List<string> Subdomain { get; set; } = new();
    public List<ExecutedToolEntity> ExecutedTools { get; set; } = new();
    public List<ExecutedModuleEntity> ExecutedModules { get; set; } = new();
}