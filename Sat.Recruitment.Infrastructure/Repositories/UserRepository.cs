using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;

namespace Sat.Recruitment.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _userDbContext;

    public UserRepository(UserDbContext userDbContext) => _userDbContext = userDbContext;

    public async Task<int> AddUserAsync(User user)
    {
        await _userDbContext.AddAsync(user);
        return await _userDbContext.SaveChangesAsync();
    }

    public async Task<bool> UserExistEmailPhoneAsync(string email, string phone)
    {
        return await _userDbContext.Users.AnyAsync(x => x.Email == email && x.Phone == phone);
    }

    public async Task<bool> UserExistNameAddressAsync(string name, string address)
    {
        return await _userDbContext.Users.AnyAsync(x => x.Name == name && x.Address == address);
    }
}
