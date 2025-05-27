using System.Runtime.CompilerServices;
using HabitNetworkAPI.Habits.Models;

namespace HabitNetworkAPI.Habits.Helpers
{
    public static class HabitMapper
    {
        public static HabitDto? ToHabitDto(this HabitInfo? habitInfo)
        {
            if (habitInfo != null)
            {
                return new HabitDto
                {
                    Id = habitInfo.Id,
                    HabitTitle = habitInfo.HabitTitle,
                    Description = habitInfo.Description,
                    Status = habitInfo.Status,
                    UserId = habitInfo.UserId,
                    DaysGoal = habitInfo.DaysGoal,
                    DaysProgress = habitInfo.DaysProgress,
                    LastUpdated = habitInfo?.LastUpdated,
                };
            }
            return null;
        }

        public static HabitInfo? ToHabitInfo(this HabitDto habitDto)
        {
            if (habitDto == null)
            {
                return null;
            }
            return new HabitInfo(
                habitDto.Id,
                habitDto.HabitTitle,
                habitDto.Description,
                habitDto.Status,
                habitDto.UserId,
                habitDto.DaysGoal,
                habitDto.DaysProgress,
                habitDto.LastUpdated
            );
        }
    }
}
