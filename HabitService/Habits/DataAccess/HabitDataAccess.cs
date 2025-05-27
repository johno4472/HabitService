using System.Data.SQLite;
using HabitNetworkAPI.Habits.Models;
using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.Habits.DataAccess
{
    public class HabitDataAccess : IHabitDataAccess
    {
        private const string _connectionString = "Data Source=C:/Users/john.olson/code/HabitService/HabitService/HabitNetworkDB.db";

        public async Task<bool> MatchUserIdToHabitIdAsync(int userId, int habitId)
        {
            string queryString = $"SELECT * from Habit WHERE UserId = @userId AND Id = @habitId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@habitId", habitId);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return false;
                }
            }
        }

        public async Task<HabitInfo?> GetHabitByIdAsync(int habitId)
        {
            string queryString = $"SELECT * from Habit WHERE Id = @habitId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@habitId", habitId);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string? habitTitle = reader["HabitTitle"].ToString();
                                string? description = reader["Description"].ToString();
                                int userId = Convert.ToInt32(reader["UserId"]);
                                int daysGoal = Convert.ToInt32(reader["DaysGoal"]);
                                int daysProgress = Convert.ToInt32(reader["DaysProgress"]);
                                int status = Convert.ToInt32(reader["Status"]);
                                string? lastUpdated = reader["LastUpdated"].ToString();
                                await connection.CloseAsync();
                                return new HabitInfo(id, habitTitle, description, status, userId, daysGoal, daysProgress, lastUpdated);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return null;
                }
            }
        }

        public async Task<HabitInfo?> GetHabitByUsernameAndTitleAsync(int givenUserId, string givenHabitTitle)
        {
            string queryString = $"SELECT * from Habit WHERE UserId = @userId AND HabitTitle = @habitTitle";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@habitTitle", givenHabitTitle);
                    command.Parameters.AddWithValue("@userId", givenUserId);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string? habitTitle = reader["HabitTitle"].ToString();
                                string? description = reader["Description"].ToString();
                                int userId = Convert.ToInt32(reader["UserId"]);
                                int daysGoal = Convert.ToInt32(reader["DaysGoal"]);
                                int daysProgress = Convert.ToInt32(reader["DaysProgress"]);
                                int status = Convert.ToInt32(reader["Status"]);
                                string? lastUpdated = reader["LastUpdated"].ToString();
                                await connection.CloseAsync();
                                return new HabitInfo(id, habitTitle, description, status, userId, daysGoal, daysProgress, lastUpdated);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return null;
                }
            }
        }

        public async Task CreateNewHabitAsync(int userId, HabitCreationInfo habitCreationInfo)
        {

            string queryString = $"INSERT INTO Habit (HabitTitle, Description, UserId, DaysGoal, Status) " +
                $"VALUES(@habitTitle, @description, @userId, @daysGoal, @status)";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@habitTitle", habitCreationInfo.HabitTitle);
                    command.Parameters.AddWithValue("@description", habitCreationInfo.Description);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@daysGoal", habitCreationInfo.DaysGoal);
                    command.Parameters.AddWithValue("@status", habitCreationInfo.Status);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public async Task DeleteHabitAsync(int habitId)
        {
            string queryString = $"DELETE FROM Habit WHERE Id = @habitId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@habitId", habitId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public async Task UpdateHabitProgressAsync(int habitId, HabitInfo habit)
        {
            string queryString = $"UPDATE Habit SET DaysProgress = @daysProgress, LastUpdated = @lastUpdated WHERE Id = @habitId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@daysProgress", habit.DaysProgress);
                    command.Parameters.AddWithValue("@lastUpdated", habit.LastUpdated);
                    command.Parameters.AddWithValue("@habitId", habitId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public async Task UpdateHabitStatusAsync(int habitId, HabitInfo habit)
        {
            string queryString = $"UPDATE Habit SET Status = @status WHERE Id = @habitId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@status", habit.Status);
                    command.Parameters.AddWithValue("@habitId", habitId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
