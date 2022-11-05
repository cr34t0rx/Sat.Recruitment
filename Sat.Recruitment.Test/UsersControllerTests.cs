using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Testing;
using MockQueryable.Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Application.Services;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Utils;
using Xunit;

namespace Sat.Recruitment.Test;

[CollectionDefinition("UsersController Tests", DisableParallelization = true)]
public class UsersControllerTests
{
    private readonly WebApplicationFactory<UsersController> _application;

    public UsersControllerTests()
    {
        _application = new WebApplicationFactory<UsersController>();
    }

    [Theory]
    [InlineData("", "pedro@gmail.com", "direccion 1", "+58123123", "Normal", 123)]
    [InlineData("Pablo", "pablogmail.com", "direccion 2", "+58123124", "SuperUser", 123)]
    [InlineData("Ana", "ana@gmail.com", "direccion 3", "+58123125", "Premium3", 123)]
    public async Task ControllerModelValidation(string name, string email, string address, string phone, string userType, decimal money)
    {
        var httpClient = _application.CreateClient();

        var response = await httpClient.PostAsJsonAsync(
            $"/Users/create-user",
            new UserModel()
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("Pedro", "pedro@gmail.com", "direccion 1", "+58123123", "Normal", 123)]
    [InlineData("Pablo", "pablo@gmail.com", "direccion 2", "+58123124", "SuperUser", 123)]
    [InlineData("Ana", "ana@gmail.com", "direccion 3", "+58123125", "Premium", 123)]
    public async Task SuccessfulCreation(string name, string email, string address, string phone, string userType, decimal money)
    {
        var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        var users = FileUserReader.ReadAllUsers(path);
        var mock = users.AsQueryable().BuildMockDbSet();

        var userRepository = new TestUserRepository(mock.Object);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserModel>();
            cfg.CreateMap<UserModel, User>();
        });
        var mapper = mockMapper.CreateMapper();

        var userService = new UserService(userRepository, mapper);

        var newUser = new UserModel()
        {
            Name = name,
            Email = email,
            Address = address,
            Phone = phone,
            UserType = userType,
            Money = money
        };

        var result = await userService.AddUserAsync(newUser);

        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("Juan", "Juan@marmol.com", "Peru 2464", "+5491154762312", "Normal", 1234)]
    [InlineData("Franco", "Franco.Perez@gmail.com", "Alvear y Colombres", "+534645213542", "Premium", 112234)]
    [InlineData("Agustina", "Agustina@gmail.com", "Garay y Otra Calle", "+534645213542", "SuperUser", 112234)]
    public async Task SuccessfulDuplicateValidation(string name, string email, string address, string phone, string userType, decimal money)
    {
        var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        var users = FileUserReader.ReadAllUsers(path);
        var mock = users.AsQueryable().BuildMockDbSet();

        var userRepository = new TestUserRepository(mock.Object);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserModel>();
            cfg.CreateMap<UserModel, User>();
        });
        var mapper = mockMapper.CreateMapper();

        var userService = new UserService(userRepository, mapper);

        var newUser = new UserModel()
        {
            Name = name,
            Email = email,
            Address = address,
            Phone = phone,
            UserType = userType,
            Money = money
        };

        var result = await userService.AddUserAsync(newUser);

        Assert.False(result.IsSuccess);
    }
}
