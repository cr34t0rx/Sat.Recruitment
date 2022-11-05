using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test;

internal class TestUserRepository : IUserRepository
{
    private readonly DbSet<User> _dbSet;
    public TestUserRepository(DbSet<User> dbSet)
    {
        _dbSet = dbSet;
    }

    public async Task<int> AddUserAsync(User user)
    {
        await _dbSet.AddAsync(user);
        return 1;
    }

    public async Task<bool> UserExistEmailPhoneAsync(string email, string phone)
    {
        return await _dbSet.AnyAsync(x => x.Email == email && x.Phone == phone);
    }

    public async Task<bool> UserExistNameAddressAsync(string name, string address)
    {
        return await _dbSet.AnyAsync(x => x.Name == name && x.Address == address);
    }
}
