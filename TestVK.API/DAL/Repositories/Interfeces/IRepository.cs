using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Interfeces;

public interface IRepository<T>
    : IDisposable
    where T : class
{
    Task<T?> GetAsync(Guid id);
    Task CreateAsync(T item);
    void Update(T item);
    Task DeleteAsync(Guid id);
    Task SaveAsync();
}