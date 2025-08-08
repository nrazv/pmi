
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using pmi.Data;

namespace pmi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly PmiDbContext dbContext;
    internal DbSet<T> dbSet;

    public Repository(PmiDbContext pmiDbContext)
    {
        dbContext = pmiDbContext;
        this.dbSet = dbContext.Set<T>();
    }

    public async Task Add(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task Delete(T entity)
    {
        dbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRange(IEnumerable<T> range)
    {
        dbSet.RemoveRange(range);
        await dbContext.SaveChangesAsync();
    }

    public Task<T?> Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IQueryable<T> query = dbSet;
        return await query.ToListAsync();
    }
}