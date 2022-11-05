using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Domain.Repositories;

public interface IUserRepository
{
    Task<int> AddUserAsync(User user);
    Task<bool> UserExistEmailPhoneAsync(string email, string phone);
    Task<bool> UserExistNameAddressAsync(string name, string address);
}
