using Sat.Recruitment.Application.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services;

public interface IUserService
{
    Task<ControllerResultModel> AddUserAsync(UserModel user);
}
