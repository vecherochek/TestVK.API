using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public abstract class Repository<T> : IRepository<T>
    where T : class
{
    protected bool Disposed = false;

    public abstract Task<T> GetAsync(Guid id);
    public abstract Task CreateAsync(T item);
    public abstract void Update(T item);
    public abstract Task DeleteAsync(Guid id);
    public abstract Task SaveAsync();

    ~Repository()
    {
        Dispose(false);
    }

    protected abstract void Dispose(bool dispose);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}