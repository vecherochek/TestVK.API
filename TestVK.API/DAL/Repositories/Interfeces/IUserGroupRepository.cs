using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Interfeces;

public interface IUserGroupRepository: IRepository<UserGroup>
{
    UserGroup GetUserGroupByCode(string code);

}