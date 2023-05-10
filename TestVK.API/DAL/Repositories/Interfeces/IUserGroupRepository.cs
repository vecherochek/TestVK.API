using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Interfeces;

public interface IUserGroupRepository: IRepository<UserGroup>
{
    Task<UserGroup> GetUserGroupByCodeAsync(string code);

}