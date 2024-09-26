namespace pmi.Project.Models;

public class ProjectEntity
{
    public string Id { get; init; }
    public string Name { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime? LastUpdated { get; init; }

    public ProjectEntity() { }

    public ProjectEntity(string id, string name, DateTime createdDate, DateTime? lastUpdated)
    {
        Id = id;
        Name = name;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
    }

    public void Deconstruct(
        out string id,
        out string name,
        out DateTime createdDate,
        out DateTime? lastUpdated
     ) => (id, name, createdDate, lastUpdated) = (Id, Name, CreatedDate, LastUpdated);
}


