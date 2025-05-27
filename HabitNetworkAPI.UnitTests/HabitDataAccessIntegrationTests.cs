using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using HabitNetworkAPI.Habits.DataAccess;
using HabitNetworkAPI.Habits.Models;
using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.UnitTests
{
    public class HabitDataAccessIntegrationTests
    {
        [Fact]
        public async Task CreateUpdateProgressUpdateStatusRetrieveDeleteHabit()
        {
            //Arrange
            var sut = new HabitDataAccess();
            int userId = 0;
            int habitId;

            HabitCreationInfo creationInfo = new HabitCreationInfo("Newhabit", "Do this new habit", 0, 30, "");
            //Delete habit if already exists
            HabitInfo? existingHabit = await sut.GetHabitByUsernameAndTitleAsync(userId, creationInfo.HabitTitle);
            if (existingHabit != null)
            {
                await sut.DeleteHabitAsync(existingHabit.Id);
            }

            //Create
            await sut.CreateNewHabitAsync(userId, creationInfo);

            //Fetch by username and habitTitle
            HabitInfo? habitGotByUserIdAndTitle = await sut.GetHabitByUsernameAndTitleAsync(userId, creationInfo.HabitTitle);
            //Check if found  
            habitGotByUserIdAndTitle.Should().NotBeNull();
            habitGotByUserIdAndTitle.UserId.Should().Be(userId);
            habitId = habitGotByUserIdAndTitle.Id;

            //Update Progress
            await sut.UpdateHabitProgressAsync(habitId, new HabitInfo(0, null, null, 0, 0, 0, 4, "Updated!"));
            //Fetch by Id
            HabitInfo? habitGotById = await sut.GetHabitByIdAsync(habitId);
            //Check if found
            habitGotById.Should().NotBeNull();
            habitGotById.DaysProgress.Should().Be(4);
            habitGotById.LastUpdated.Should().Be("Updated!");

            //Update Status
            await sut.UpdateHabitStatusAsync(habitId, new HabitInfo(0, null, null, 2, 0, 0, 0, null));
            //Fetch by Id
            HabitInfo? habitGotByIdAgain = await sut.GetHabitByIdAsync(habitId);
            //Check if found
            habitGotByIdAgain.Should().NotBeNull();
            habitGotByIdAgain.DaysProgress.Should().Be(4);
            habitGotByIdAgain.LastUpdated.Should().Be("Updated!");
            habitGotByIdAgain.Status.Should().Be(2);

            //Delete by Id
            await sut.DeleteHabitAsync(habitId);
            //Check if Deleted
            (await sut.GetHabitByIdAsync(habitId)).Should().BeNull();
        }
    }
}
