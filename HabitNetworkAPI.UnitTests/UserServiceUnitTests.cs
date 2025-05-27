using NSubstitute;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using HabitNetworkAPI.Users.DataAccess;
using HabitNetworkAPI.Users.Models;
using HabitNetworkAPI.Users.Service;

namespace HabitNetworkAPI.UnitTests
{
    public class UserServiceUnitTests
    {
        private IUserDataAccess _userDataAccess = Substitute.For<IUserDataAccess>();

        [Fact]
        public async Task NullReturnedWhenUserNotFound()
        {
            //Arrange
            _userDataAccess.GetUserInfoAsync(Arg.Any<int>()).ReturnsNull();
            var sut = new UserService(_userDataAccess);

            //Act
            var userDto = await sut.GetUserInfoAsync(123);

            //Assert
            userDto.Should().BeNull();
            await _userDataAccess.Received(1).GetUserInfoAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task UserReturnedWhenUserFound()
        {
            //Arrange
            _userDataAccess.GetUserInfoAsync(123).Returns(new UserInfo(123, "Jimmy", "Jimmy@email.com"));
            var sut = new UserService(_userDataAccess);

            //Act
            var userDto = await sut.GetUserInfoAsync(123);

            //Assert
            userDto.Should().NotBeNull();
            await _userDataAccess.Received(1).GetUserInfoAsync(Arg.Any<int>());
        }

        /*
        [Fact]
        public async Task UserCreationReturnsNullWhenExistingUserFound()
        {
            //Arrange
            _userDataAccess.GetUserByUsernameAsync("Jimmy").Returns(new UserInfo(123, "Jimmy", "Jimmy@email.com"));
            var sut = new UserService(_userDataAccess);

            //Act
            var userDto = await sut.CreateUserByUsernameEmail("Jimmy", "Jimmy@email.com");

            //Assert
            userDto.Should().BeNull();
            await _userDataAccess.Received(1).GetUserByUsernameAsync(Arg.Any<string>());
            await _userDataAccess.Received(0).CreateUserAsync(Arg.Any<string>(), Arg.Any<string>());
        }*/

        [Fact]
        public async Task UserCreatedAndReturnedWhenNoExistingUserFound()
        {
            //Arrange
            _userDataAccess.GetUserByUsernameAsync("Jimmy").Returns(new UserInfo(111, "Jimmy", "Jimmy@email.com"));
            var sut = new UserService(_userDataAccess);

            //Act
            var userDto = await sut.CreateUserByUsernameEmail("Jimmy", "Jimmy@email.com");

            //Assert
            userDto.Should().NotBeNull();
            await _userDataAccess.Received(1).GetUserByUsernameAsync(Arg.Any<string>());
            await _userDataAccess.Received(1).CreateUserAsync(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}