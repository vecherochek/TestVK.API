using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
{
    private UserInfoDbContext DbContext { get; set; }
    public DbSet<UserGroup> UserGroups => DbContext.UserGroups;
    public UserGroupRepository(UserInfoDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public override UserGroup? Get(Guid id)
    {
        return UserGroups
            .FirstOrDefault(userGroup => userGroup.Id == id);
    }

    public override void Create(UserGroup item)
    {
        UserGroups.Add(item);
    }

    public override void Update(UserGroup item)
    {
        UserGroups.Update(item);
    }

    public override void Delete(Guid id)
    {
        var userGroup = Get(id);
        if (userGroup != null)
            UserGroups.Remove(userGroup);
    }

    public override void Save()
    {
        DbContext.SaveChanges();
    }

    public UserGroup? GetUserGroupByCode(string code)
    {
        return UserGroups
            .FirstOrDefault(userGroup => userGroup.Code == code);
    }

    protected override void Dispose(bool dispose)
    {
        if (Disposed)
            return;

        if (dispose)
        {
            DbContext.Dispose();
        }

        DbContext = null;
    }
}