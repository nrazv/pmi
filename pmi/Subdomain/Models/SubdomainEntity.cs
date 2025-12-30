
using System.Text.Json.Serialization;
using pmi.Project.Models;

namespace pmi.Subdomain.Models;

public class SubdomainEntity
{
    public Guid Id { get; set; }
    public required string Domain { get; set; }
    public bool Validated { get; set; } = false;

    public string? ValidatedBy { get; set; }
    public string? ValidationResult { get; set; }

    public Guid ProjectId { get; set; }

    [JsonIgnore]
    public ProjectEntity? Project { get; set; } = null!;
}