using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Interfeces;

public interface IUserRepository: IRepository<User>
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User>? GetUserByLoginAsync(string login);
    Task<User>? GetUserByGroupAsync(Guid groupId);
}