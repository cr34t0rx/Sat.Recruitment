using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Domain.Repositories;
using Sat.Recruitment.Domain.Models;
using AutoMapper;

namespace Sat.Recruitment.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ControllerResultModel> AddUserAsync(UserModel userModel)
    {
        //first validation: match email and phone
        if (await _userRepository.UserExistEmailPhoneAsync(userModel.Email, userModel.Phone))
        {
            return new ControllerResultModel
            {
                Message = "User is duplicated"
            };
        }
        //second validation: match name and address
        if (await _userRepository.UserExistNameAddressAsync(userModel.Name, userModel.Address))
        {
            return new ControllerResultModel
            {
                Message = "User is duplicated"
            };
        }

        if (userModel.UserType == "Normal")
        {
            if (userModel.Money > 100)
            {
                //If new user is normal and has more than USD100
                userModel.Money = GenerateGif(userModel.Money, 0.12m);
            }
            if (userModel.Money > 10 && userModel.Money < 100)
            {
                userModel.Money = GenerateGif(userModel.Money, 0.8m);
            }
        }
        if (userModel.UserType == "SuperUser")
        {
            if (userModel.Money > 100)
            {
                userModel.Money = GenerateGif(userModel.Money, 0.20m);
            }
        }
        if (userModel.UserType == "Premium")
        {
            if (userModel.Money > 100)
            {
                userModel.Money = GenerateGif(userModel.Money, 2m);
            }
        }

        //map user model to user database
        var userDb = _mapper.Map<User>(userModel);
        //create the new user
        await _userRepository.AddUserAsync(userDb);

        return new ControllerResultModel
        {
            IsSuccess = true,
            Message = "User Created"
        };
    }

    private decimal GenerateGif(decimal originalAmount, decimal gifPercentage)
    {
        var gif = originalAmount * gifPercentage;
        return originalAmount + gif;
    }
}
