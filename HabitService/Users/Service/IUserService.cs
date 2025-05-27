using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.Users.Service
{
    public interface IUserService
    {
        Task<UserDto?> GetUserInfoAsync(int userId);

        Task<UserDto?> CreateUserByUsernameEmail(string username, string email);

    }
}
