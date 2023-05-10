using TestVK.API.BLL.Models;

namespace TestVK.API.BLL.Services.Interfaces;

public interface IUserService
{
    User GetUser(Guid id);
    IEnumerable<User> GetAllUsers();
    void DeleteUser(Guid id);
    void CreateNewUser(string login, byte[] password, string userGroupCode);
}