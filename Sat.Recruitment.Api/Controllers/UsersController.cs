using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Application.Services;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public partial class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpPost]
    [Route("create-user")]
    [ProducesResponseType(typeof(ControllerResultModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ControllerResultModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] UserModel userData)
    {
        var result = await _userService.AddUserAsync(userData);

        if (result.IsSuccess)
        {
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }
        else
        {
            return BadRequest(result);
        }
    }
}
