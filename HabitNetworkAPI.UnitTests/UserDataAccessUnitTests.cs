using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using HabitNetworkAPI.Users.DataAccess;
using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.UnitTests
{
    public class UserDataAccessUnitTests
    {
        [Fact]
        public async Task CreateThenGetUserByNameThenId()
        {
            //Arrange
            var sut = new UserDataAccess();
            UserInfo? userGotById = null;
            UserInfo? userGotByUsername = null;
            string username = "TestUser";

            //Create
            await sut.CreateUserAsync(username, "TestEmail");

            //Fetch by username
            userGotByUsername = await sut.GetUserByUsernameAsync(username);
            //Check if found
            userGotByUsername.Should().NotBeNull();
            userGotByUsername.Username.Should().Be(username);

            //Fetch by Id
            userGotById = await sut.GetUserInfoAsync(userGotByUsername.UserId);
            //Check if found
            userGotById.Should().NotBeNull();
            userGotById.UserId.Should().Be(userGotByUsername.UserId);

            //Delete by Id
            await sut.DeleteUserAsync(userGotByUsername.UserId);
            //Check if Deleted
            (await sut.GetUserInfoAsync(userGotByUsername.UserId)).Should().BeNull();
        }
    }
}
