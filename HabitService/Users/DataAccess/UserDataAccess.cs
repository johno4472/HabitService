using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Dapper;
using HabitNetworkAPI.Users.Models;

namespace HabitNetworkAPI.Users.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private const string _connectionString = "Data Source=C:/Users/john.olson/code/HabitService/HabitService/HabitNetworkDB.db";
        public async Task<UserInfo?> GetUserInfoAsync(int userId)
        {
            string queryString = $"SELECT * from User WHERE UserId = @userId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                            if (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["UserId"]);
                                string? username = reader["Username"]?.ToString();
                                string? email = reader["Email"]?.ToString();
                                await connection.CloseAsync();
                                return new UserInfo(id, username ?? "", email ?? "");
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

        public async Task<UserInfo?> GetUserByUsernameAsync(string givenUsername)
        {
            string queryString = $"SELECT * from User WHERE Username = @givenUsername";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("givenUsername", givenUsername);

                    try
                    {
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int id = Convert.ToInt32(reader["UserId"]);
                                string? username = reader["Username"]?.ToString();
                                string? email = reader["Email"]?.ToString();
                                await connection.CloseAsync();
                                return new UserInfo(id, username ?? "", email ?? "");
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

        public async Task CreateUserAsync(string givenUsername, string givenEmail)
        {
            string queryString = $"INSERT INTO User (Username, Email) Values(@Username, @Email)";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Username", givenUsername);
                    command.Parameters.AddWithValue("@Email", givenEmail);

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

        public async Task DeleteUserAsync(int givenId)
        {
            string queryString = $"DELETE FROM User WHERE UserId = @givenId";

            using (SQLiteConnection connection = new(_connectionString))
            {
                using (SQLiteCommand command = new(queryString, connection))
                {
                    command.Parameters.AddWithValue("@givenId", givenId);

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
