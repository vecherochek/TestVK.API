using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;
using TestVK.API.DAL.Repositories.Contexts;
using TestVK.API.DAL.Repositories.Interfeces;

namespace TestVK.API.DAL.Repositories;

public class UserRepository: Repository<User>, IUserRepository
{
    private UserInfoDbContext DbContext { get; set; }

    private DbSet<User> Users => DbContext.Users;
    public UserRepository(UserInfoDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public override User? Get(Guid id)
    {
        return Users
            .FirstOrDefault(user => user.Id == id);
    }

    public override void Create(User item)
    {
        Users.Add(item);
    }

    public override void Update(User item)
    {
        Users.Update(item);
    }

    public override void Delete(Guid id)
    {
        var user = Get(id);
        if (user != null)
            Users.Remove(user);
    }

    public override void Save()
    {
        DbContext.SaveChanges();
    }

    public IEnumerable<User> GetUsers()
    {
        return Users.ToList();
    }

    public User? GetUserByLogin(string login)
    {
        return Users
            .FirstOrDefault(user => user.Login == login);
    }

    public User? GetUserByGroup(Guid groupId)
    {
        return Users
            .FirstOrDefault(user => user.UserGroupId == groupId);
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