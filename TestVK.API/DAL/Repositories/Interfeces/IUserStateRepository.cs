using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Interfeces;

public interface IUserStateRepository: IRepository<UserState>
{
    UserState GetUserStateByCode(string code);
}