namespace HabitNetworkAPI.Habits.Models
{
    public class HabitDto
    {
        public int Id { get; set; }
        public string? HabitTitle { get; set; } = "";
        public string? Description { get; set; } = "";
        public int Status { get; set; }
        public int UserId { get; set; }
        public int DaysGoal { get; set; }
        public int DaysProgress { get; set; }
        public string? LastUpdated { get; set; } = ""; 
    }
}
