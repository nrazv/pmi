using pmi.Data;
using pmi.Project.Models;
using pmi.Repository;

namespace pmi.Project.Repository;


public class ProjectRepository : Repository<ProjectEntity>, IProjectRepository
{
    private readonly PmiDbContext dbContext;
    public ProjectRepository(PmiDbContext pmiDbContext) : base(pmiDbContext)
    {
        dbContext = pmiDbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<ProjectEntity> Update(ProjectEntity obj)
    {
        dbContext.Update(obj);
        await dbContext.SaveChangesAsync();
        return obj;
    }
}