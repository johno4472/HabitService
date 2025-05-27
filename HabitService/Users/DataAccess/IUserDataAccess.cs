using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.Users.DataAccess
{
    public interface IUserDataAccess
    {
        Task<UserInfo?> GetUserInfoAsync(int userID);

        Task<UserInfo?> GetUserByUsernameAsync(string username);

        Task CreateUserAsync(string username, string email);

        Task DeleteUserAsync(int givenId);
    }
}
