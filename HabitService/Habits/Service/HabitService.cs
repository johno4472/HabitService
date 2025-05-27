using HabitNetworkAPI.Habits.DataAccess;
using HabitNetworkAPI.Habits.Models;
using HabitNetworkAPI.Habits.Helpers;

namespace HabitNetworkAPI.Habits.Service
{
    public class HabitService
    {
        private readonly IHabitDataAccess _habitDataAccess;

        public HabitService(IHabitDataAccess habitDataAccess)
        {
            _habitDataAccess = habitDataAccess;
        }

        public async Task<HabitDto?> GetHabitInfoAsync(int userId, int habitId)
        {
            //check if habitID belongs to requesting user
            if (await _habitDataAccess.MatchUserIdToHabitIdAsync(userId, habitId))
            {
                var habitInfo = await _habitDataAccess.GetHabitByIdAsync(habitId);

                return habitInfo.ToHabitDto();
            }
            return null;
        }

        public async Task CreateNewHabitAsync(int userId, HabitCreationInfo habitCreationInfo)
        {
            await _habitDataAccess.CreateNewHabitAsync(userId, habitCreationInfo);
        }

        public async Task DeleteExistingHabitAsync(int userId, int habitId)
        {
            //check if user and habit correspond
            if (await _habitDataAccess.MatchUserIdToHabitIdAsync(userId, habitId))
            {
                await _habitDataAccess.DeleteHabitAsync(habitId);
            }
        }

        public async Task UpdateExistingHabit(int userId, int habitId, HabitDto habit)
        {
            if (habit == null)
            {
                throw new ArgumentNullException(nameof(habit));
            }
            if (habitId == habit.Id)
            {
                if (await _habitDataAccess.MatchUserIdToHabitIdAsync(userId, habitId))
                {
                    await _habitDataAccess.UpdateHabitProgressAsync(habitId, habit.ToHabitInfo());
                }
            }
        }
    }
}
