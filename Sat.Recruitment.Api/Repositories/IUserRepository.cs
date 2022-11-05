using Sat.Recruitment.Api.Models.Database;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories;

public interface IUserRepository
{
    Task<int> AddUserAsync(User user);
    Task<bool> UserExistEmailPhoneAsync(string email, string phone);
    Task<bool> UserExistNameAddressAsync(string name, string address);
}
