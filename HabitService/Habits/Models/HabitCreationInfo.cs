namespace HabitNetworkAPI.Habits.Models
{
    public record HabitCreationInfo(string HabitTitle, string Description, int Status, int DaysGoal, string LastUpdated);
}
