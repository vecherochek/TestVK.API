using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public class UserStateRepository: Repository<UserState>, IUserStateRepository
{
    private UserInfoDbContext DbContext { get; set; }

    private DbSet<UserState> UserStates => DbContext.UserStates;
    public UserStateRepository(UserInfoDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public override async Task<UserState?> GetAsync(Guid id)
    {
        return await UserStates
            .FirstOrDefaultAsync(userState => userState.Id == id);
    }

    public override async Task CreateAsync(UserState item)
    {
        await UserStates.AddAsync(item);
    }

    public override void Update(UserState item)
    {
        UserStates.Update(item);
    }

    public override async Task DeleteAsync(Guid id)
    {
        var userState = await GetAsync(id);
        if (userState != null)
            UserStates.Remove(userState);
    }

    public override async Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public async Task<UserState?> GetUserStateByCodeAsync(string code)
    {
        return await UserStates
            .FirstOrDefaultAsync(userState => userState.Code == code);
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