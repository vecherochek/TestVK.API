using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public abstract class Repository<T> : IRepository<T>
    where T : class
{
    protected bool Disposed = false;

    public abstract T Get(Guid id);
    public abstract void Create(T item);
    public abstract void Update(T item);
    public abstract void Delete(Guid id);
    public abstract void Save();

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