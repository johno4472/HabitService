using HabitNetworkAPI.Habits.Models;

namespace HabitNetworkAPI.Habits.Service
{
    public interface IHabitService
    {
        Task<HabitDto?> GetHabitInfoAsync(int userId, int habitId);

        Task<HabitDto?> CreateNewHabitAsync(int userId, HabitCreationInfo habitCreationInfo);

        Task<bool> DeleteExistingHabitAsync(int userId, int habitId);

        Task<HabitDto?> UpdateExistingHabit(int userId, int habitId, HabitDto habit);
    }
}
