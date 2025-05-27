using NSubstitute;
using HabitNetworkAPI.Habits.DataAccess;
using HabitNetworkAPI.Habits.Models;
using HabitNetworkAPI.Habits.Service;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NSubstitute.ReturnsExtensions;

namespace HabitNetworkAPI.UnitTests
{
    public class HabitServiceUnitTests
    {
        private readonly IHabitDataAccess _habitDataAccess = Substitute.For<IHabitDataAccess>();

        [Fact]
        public async Task NullReturnedWhenIdOfHabitOwnerNotFoundOrDoesNotMatch()
        {
            //Arrange
            _habitDataAccess.MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(false);
            var sut = new HabitService(_habitDataAccess);

            //Act
            var userDto = await sut.GetHabitInfoAsync(123, 321);

            //Assert
            userDto.Should().BeNull();
            await _habitDataAccess.Received(1).MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>());
            await _habitDataAccess.Received(0).GetHabitByIdAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task HabitReturnedWhenHabitFoundAndMatches()
        {
            //Arrange
            _habitDataAccess.MatchUserIdToHabitIdAsync(123, 321).Returns(true);
            _habitDataAccess.GetHabitByIdAsync(321).Returns(new HabitInfo(321, "TestHabit", "I test things", 1, 123, 30, 25, "Yesterday"));
            var sut = new HabitService(_habitDataAccess);

            //Act
            var userDto = await sut.GetHabitInfoAsync(123, 321);

            //Assert
            userDto.Should().NotBeNull();
            await _habitDataAccess.Received(1).MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>());
            await _habitDataAccess.Received(1).GetHabitByIdAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task HabitDeletedWhenFoundAndMatches()
        {
            //Arrange
            _habitDataAccess.MatchUserIdToHabitIdAsync(123, 123).Returns(true);
            var sut = new HabitService( _habitDataAccess);

            //Act
            await sut.DeleteExistingHabitAsync(123, 123);

            //Assert
            await _habitDataAccess.Received(1).MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>());
            await _habitDataAccess.Received(1).DeleteHabitAsync(Arg.Any<int>());
        }

        public HabitDto CreateSampleHabitDtoById(int userId, int habitId)
        {
            return new HabitDto
            {
                Id = habitId,
                HabitTitle = "Test",
                Description = "Test",
                Status = 0,
                UserId = userId,
                DaysGoal = 0,
                DaysProgress = 0,
            };
        }

        [Fact]
        public async Task NullReturnedWhenGivenIdAndHabitIdDoNotMatch()
        {
            //Arrange
            var sut = new HabitService(_habitDataAccess);

            //Act
            await sut.UpdateExistingHabit(111, 222, CreateSampleHabitDtoById(111, 333));

            //Assert
            await _habitDataAccess.Received(0).MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>());
            await _habitDataAccess.Received(0).UpdateHabitProgressAsync(Arg.Any<int>(), Arg.Any<HabitInfo>());
        }

        [Fact]
        public async Task NullReturnedWhenUserIdAndHabitUserDoNotMatch()
        {
            //Arrange
            _habitDataAccess.MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(false);
            var sut = new HabitService(_habitDataAccess);

            //Act
            await sut.UpdateExistingHabit(111, 222, CreateSampleHabitDtoById(111, 222));

            //Assert
            await _habitDataAccess.Received(1).MatchUserIdToHabitIdAsync(Arg.Any<int>(), Arg.Any<int>());
            await _habitDataAccess.Received(0).UpdateHabitProgressAsync(Arg.Any<int>(), Arg.Any<HabitInfo>());
        }
        /*
        [Fact]
        public async NullReturnedWhenHabitToBeUpdatedNotFound()
        {
            //Arrange
            _habitDataAccess.MatchUserIdToHabitId(Arg.Any<int>(), Arg.Any<int>()).Returns(false);
            var sut = new HabitService(_habitDataAccess);

            //Act
            var habitDto = await sut.UpdateExistingHabit(111, 222, CreateSampleHabitDtoById(111, 222));

            //Assert
            habitDto.Should().BeNull();
            await _habitDataAccess.Received(1).MatchUserIdToHabitId(Arg.Any<int>(), Arg.Any<int>());
            await _habitDataAccess.Received(0).UpdateHabit(Arg.Any<int>(), Arg.Any<HabitInfo>());
        }*/
    }
}
