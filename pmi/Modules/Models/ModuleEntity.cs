using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using pmi.Tool.Models;

namespace pmi.Modules.Models;

[Table("Modules")]
[Index(nameof(Name), IsUnique = true)]
public class ModuleEntity
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public List<ExecutableTool> ExecutablesTools { get; set; } = new();
}