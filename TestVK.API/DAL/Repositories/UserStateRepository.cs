using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public class UserStateRepository: Repository<UserState>, IUserStateRepository
{
    private UserInfoDbContext DbContext { get; set; }

    public DbSet<UserState> UserStates => DbContext.UserStates;
    public override UserState? Get(Guid id)
    {
        return UserStates
            .FirstOrDefault(userState => userState.Id == id);
    }

    public override void Create(UserState item)
    {
        UserStates.Add(item);
    }

    public override void Update(UserState item)
    {
        UserStates.Update(item);
    }

    public override void Delete(Guid id)
    {
        var userState = Get(id);
        if (userState != null)
            UserStates.Remove(userState);
    }

    public override void Save()
    {
        DbContext.SaveChanges();
    }

    public UserState? GetUserStateByCode(string code)
    {
        return UserStates
            .FirstOrDefault(userState => userState.Code == code);
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