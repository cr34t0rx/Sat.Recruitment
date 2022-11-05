using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Utils;
using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Infrastructure;

public class UserDbContext : DbContext
{
    public UserDbContext()
    {

    }

    public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
    {
        Database.EnsureCreated();
        Database.Migrate();
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //load initial data from Users.txt
        var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        var seedData = FileUserReader.ReadAllUsers(path);

        if (seedData != null)
            modelBuilder.Entity<User>().HasData(seedData);

        base.OnModelCreating(modelBuilder);
    }
}
