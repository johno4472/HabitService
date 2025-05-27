using HabitNetworkAPI.Users.Models;
using HabitNetworkAPI.Users.DataAccess;
using HabitNetworkAPI.Users.Helpers;
using System.Reflection.Metadata.Ecma335;

namespace HabitNetworkAPI.Users.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserService(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<UserDto?> GetUserInfoAsync(int userId)
        {
            var userInfo = await _userDataAccess.GetUserInfoAsync(userId);

            if (userInfo == null)
            {
                return null;
            }

            return userInfo.ToUserDto();
        }


        public async Task<UserDto?> CreateUserByUsernameEmail(string username, string email)
        {
            await _userDataAccess.CreateUserAsync(username, email);

            var userInfo = await _userDataAccess.GetUserByUsernameAsync(username);

            return userInfo?.ToUserDto();
        }
    }
}
