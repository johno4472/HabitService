using System.Runtime.CompilerServices;
using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.Users.Helpers
{
    public static class UserMapper
    {
        public static UserDto? ToUserDto(this UserInfo userInfo)
        {
            if (userInfo == null)
            {
                return null;

            }
            return new UserDto(userInfo.UserId, userInfo.Username, userInfo.Email);
        }
    }
}
