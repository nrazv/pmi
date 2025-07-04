﻿using System.ComponentModel.DataAnnotations;
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
    public ProjectInfo ProjectInfo { get; set; } = null!;
    public ICollection<ExecutedToolEntity> ExecutedTools { get; } = new List<ExecutedToolEntity>();


    public ProjectEntity()
    {
    }

    public ProjectEntity(string id, string name, string domainName, string? ipAddress, ProjectInfo projectInfo)
    {
        Id = id;
        Name = name;
        DomainName = domainName;
        IpAddress = ipAddress;
        ProjectInfo = projectInfo;
    }
    public void Deconstruct(
        out string id,
        out string name
     ) => (id, name) = (Id, Name);
}


