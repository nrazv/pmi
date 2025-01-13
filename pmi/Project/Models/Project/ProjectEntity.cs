using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace pmi.Project.Models;


[Index(nameof(Name), IsUnique = true)]
public class ProjectEntity
{
    [Key]
    public string Id { get; init; } = null!;
    public ProjectInfo ProjectInfo { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? DomainName { get; set; }
    public string? IpAddress { get; set; }
    public List<ExecutedToolEntity> ExecutedTools { get; set; } = new();


    public ProjectEntity(string id, string name, string domainName, string? ipAddress, ProjectInfo projectInfo)
    {
        Id = id;
        Name = name;
        DomainName = domainName;
        IpAddress = ipAddress;
        ProjectInfo = projectInfo;
    }

    public ProjectEntity() { }

    public void Deconstruct(
        out string id,
        out string name
     ) => (id, name) = (Id, Name);
}


