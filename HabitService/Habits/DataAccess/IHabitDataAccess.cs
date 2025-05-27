using HabitNetworkAPI.Habits.Models;

namespace HabitNetworkAPI.Habits.DataAccess
{
    public interface IHabitDataAccess
    {
        Task<bool> MatchUserIdToHabitIdAsync(int userId, int habitId);

        Task<HabitInfo?> GetHabitByIdAsync(int habitId);

        Task<HabitInfo?> GetHabitByUsernameAndTitleAsync(int userId, string habitTitle);

        Task CreateNewHabitAsync(int userId, HabitCreationInfo habitCreationInfo);

        Task DeleteHabitAsync(int habitId);

        Task UpdateHabitProgressAsync(int habitId, HabitInfo habit);

        Task UpdateHabitStatusAsync(int habitId, HabitInfo habit);
    }
}
