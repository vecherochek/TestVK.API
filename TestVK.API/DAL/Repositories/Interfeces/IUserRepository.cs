using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Interfeces;

public interface IUserRepository: IRepository<User>
{
    IEnumerable<User> GetUsers();
    User? GetUserByLogin(string login);
    User? GetUserByGroup(Guid groupId);
}