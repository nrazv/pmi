using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace pmi.Project.Models;


[Index(nameof(Name), IsUnique = true)]
public class ProjectEntity
{
    [Key]
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? DomainName { get; set; }
    public string? IpAddress { get; set; }
    public DateTime CreatedDate { get; init; }
    public DateTime? LastUpdated { get; init; }
    public List<ExecutedToolEntity> ExecutedTools { get; set; } = new();


    public ProjectEntity(string id, string name, DateTime createdDate,
                            DateTime? lastUpdated, string domainName, string? ipAddress)
    {
        Id = id;
        Name = name;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        DomainName = domainName;
        IpAddress = ipAddress;
    }

    public ProjectEntity() { }

    public void Deconstruct(
        out string id,
        out string name,
        out DateTime createdDate,
        out DateTime? lastUpdated
     ) => (id, name, createdDate, lastUpdated) = (Id, Name, CreatedDate, LastUpdated);
}


