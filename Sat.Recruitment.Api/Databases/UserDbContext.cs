using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Models.Database;
using Sat.Recruitment.Api.Utils;

namespace Sat.Recruitment.Api.Databases;

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
        var seedData = FileUserReader.ReadAllUsers();

        if (seedData != null)
            modelBuilder.Entity<User>().HasData(seedData);

        base.OnModelCreating(modelBuilder);
    }
}
