using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
{
    private UserInfoDbContext DbContext { get; set; }
    private DbSet<UserGroup> UserGroups => DbContext.UserGroups;
    public UserGroupRepository(UserInfoDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public override async Task<UserGroup?> GetAsync(Guid id)
    {
        return await UserGroups
            .FirstOrDefaultAsync(userGroup => userGroup.Id == id);
    }

    public override async Task CreateAsync(UserGroup item)
    {
        await UserGroups.AddAsync(item);
    }

    public override void Update(UserGroup item)
    {
        UserGroups.Update(item);
    }

    public override async Task DeleteAsync(Guid id)
    {
        var userGroup = await GetAsync(id);
        if (userGroup != null)
            UserGroups.Remove(userGroup);
    }

    public override async Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public async Task<UserGroup?> GetUserGroupByCodeAsync(string code)
    {
        return await UserGroups
            .FirstOrDefaultAsync(userGroup => userGroup.Code == code);
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