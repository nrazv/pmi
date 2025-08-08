using pmi.Project.Models;
using pmi.Repository;

namespace pmi.Project.Repository;

public interface IProjectRepository : IRepository<ProjectEntity>
{
    Task<ProjectEntity> Update(ProjectEntity obj);
    Task Save();
}