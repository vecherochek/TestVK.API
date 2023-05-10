using TestVK.API.BLL.Helpers;
using TestVK.API.BLL.Models;

namespace TestVK.API.BLL.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserAsync(Guid id);
    Task<List<MyPage>> GetAllUsersAsync();
    Task DeleteUserAsync(Guid id);
    Task CreateNewUserAsync(string login, byte[] password, string userGroupCode);
}