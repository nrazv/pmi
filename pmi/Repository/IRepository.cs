using System.Linq.Expressions;

namespace pmi.Repository;


public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> expression);
    Task Add(T entity);
    Task Delete(T entity);
    Task DeleteRange(IEnumerable<T> range);
}