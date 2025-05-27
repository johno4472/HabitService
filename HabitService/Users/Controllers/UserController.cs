using Microsoft.AspNetCore.Mvc;
using HabitNetworkAPI.Users.Models;
using HabitNetworkAPI.Users.DataAccess;
using HabitNetworkAPI.Users.Service;

namespace HabitNetworkAPI.Users.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userID:int}/userInfo")]
    public async Task<IResult> GetUser(int userID)
    {
        var userInfo = await _userService.GetUserInfoAsync(userID);

        return Results.Ok(userInfo);
    }

    [HttpPost("newUser")]
    public async Task<IResult> CreateUser([FromBody] UserCreationInfo userCreationInfo)
    {
        string username = userCreationInfo.username;

        var userInfo = await _userService.CreateUserByUsernameEmail(userCreationInfo.username, userCreationInfo.email);

        if (userInfo == null)
        {
            return Results.Conflict(username);
        }
        
        return Results.Ok(userInfo);
    }
}
