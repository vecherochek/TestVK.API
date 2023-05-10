using Microsoft.EntityFrameworkCore;
using TestVK.API.BLL.Models;

namespace TestVK.API.DAL.Repositories.Contexts;

public class UserInfoDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserState> UserStates { get; set; }

    public UserInfoDbContext(DbContextOptions<UserInfoDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=qwerty");
    }*/

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(options =>
        {
            options.HasKey(x => x.Id);
            options.HasIndex(e => e.Login).IsUnique();
        });

        builder.Entity<UserGroup>(options =>
        {
            options.HasKey(x => x.Id);
            options.HasMany<User>()
                .WithOne();
            options.HasData(
                new UserGroup { Id = Guid.NewGuid(), Code = "Admin", Description = "Admin" },
                new UserGroup { Id = Guid.NewGuid(), Code = "User", Description = "User" });
        });

        builder.Entity<UserState>(options =>
        {
            options.HasKey(x => x.Id);
            options.HasMany<User>()
                .WithOne();
            options.HasData(
                new UserState { Id = Guid.NewGuid(), Code = "Active", Description = "User exists" },
                new UserState { Id = Guid.NewGuid(), Code = "Blocked", Description = "User does not exist" });
        });
    }
}