namespace pmi.Service;

public interface IService<T>
{
    public Task<List<T>> GetAll();
    public Task AddNew(T obj);
    public Task<T?> GetById(string id);
    public Task<T?> GetByName(string name);
    public T Update(T obj);
}