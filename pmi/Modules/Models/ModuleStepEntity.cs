using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace pmi.Modules.Models;

[Table("ModulesSteps")]
[Index(nameof(Name), IsUnique = true)]
public class ModuleStepEntity
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string ExecutionResult { get; set; }

    [Required]
    public required Guid ModuleId { get; set; }
    [Required]
    [JsonIgnore]
    public required ModuleEntity Module { get; set; }
}