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
    public class HabitServiceDatabasemanipulation
    {
        [Fact]
        public async Task DeleteOneRowFromUser()
        {
            //Arrange
            int idOfUserToDelete = 0;
            var sut = new UserDataAccess();

            if (idOfUserToDelete == 0)
            {
                //Delete by Id
                await sut.DeleteUserAsync(idOfUserToDelete);
                //Check if Deleted
                (await sut.GetUserInfoAsync(idOfUserToDelete)).Should().BeNull();
            }
            else
            {
                idOfUserToDelete.Should().Be(0);
            }
        }

        [Fact]
        public async Task AddOneRowUser()
        {
            //Arrange
            string usernameToCreate = "";
            string email = "";
            UserInfo? userGotByUsername;
            var sut = new UserDataAccess();

            if (usernameToCreate == "")
            {
                await sut.CreateUserAsync(usernameToCreate, email);

                //Fetch by username
                userGotByUsername = await sut.GetUserByUsernameAsync(usernameToCreate);
                //Check if found
                userGotByUsername.Should().NotBeNull();
                userGotByUsername.Username.Should().Be(usernameToCreate);
            }
            else
            {
                usernameToCreate.Should().Be("");
            }
        }
    }
}
