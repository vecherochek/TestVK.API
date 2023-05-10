using TestVK.API.BLL.Models;
using TestVK.API.BLL.Structs;

namespace TestVK.API.BLL.Services.Interfaces;

public interface IUserService
{
    User GetUser(Guid id);
    List<MyPage> GetAllUsers();
    void DeleteUser(Guid id);
    void CreateNewUser(string login, byte[] password, string userGroupCode);
}