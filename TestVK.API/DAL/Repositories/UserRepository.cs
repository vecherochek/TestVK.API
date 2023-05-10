using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private UserInfoDbContext DbContext { get; set; }

    private DbSet<User> Users => DbContext.Users;

    public UserRepository(UserInfoDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public override async Task<User?> GetAsync(Guid id)
    {
        return await Users
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public override async Task CreateAsync(User item)
    {
        await Users.AddAsync(item);
    }

    public override void Update(User item)
    {
        Users.Update(item);
    }
    

    public override async Task DeleteAsync(Guid id)
    {
        var user = await GetAsync(id);
        if (user != null)
            Users.Remove(user);
    }

    public override async Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>?> GetUsersAsync()
    {
        return await Users.ToListAsync();
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        return await Users
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<User?> GetUserByGroupAsync(Guid groupId)
    {
        return await Users
            .FirstOrDefaultAsync(user => user.UserGroupId == groupId);
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