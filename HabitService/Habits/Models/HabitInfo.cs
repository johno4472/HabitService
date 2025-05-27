namespace HabitNetworkAPI.Habits.Models
{
    public class HabitInfo
    {
        public int Id { get; set; }
        public string? HabitTitle { get; set; } = "";
        public string? Description { get; set; } = "";
        public int Status { get; set; }
        public int UserId { get; set; }
        public int DaysGoal {  get; set; }
        public int DaysProgress { get; set; }
        public string? LastUpdated { get; set; } = "";

        public HabitInfo(int id, string? habitTitle, string? description, int status, int userId, int daysGoal, int daysProgress, string? lastUpdated) 
        { 
            Id = id; HabitTitle = habitTitle; Description = description; Status = status; UserId = userId; DaysGoal = daysGoal; DaysProgress = daysProgress; LastUpdated = lastUpdated;
        }
    }
}
