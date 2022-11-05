using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services;

public interface IUserService
{
    Task<ControllerResultModel> AddUserAsync(UserModel user);
}
