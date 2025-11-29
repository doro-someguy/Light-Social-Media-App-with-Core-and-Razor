using MySql.Data.MySqlClient;
using LightForum.Models;

namespace LightForum.Services
{
    public class UserService
    {
        private readonly string? _connectionString;

        public UserService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM user";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    UserId = reader.GetInt32("UserId"),
                    UserNickname = reader.GetString("UserNickname"),
                    UserRegistrationDate = reader.GetDateTime("UserRegistrationDate"),
                    UserIsVerified = reader.GetBoolean("UserIsVerified"),
                    UserNoTopics = reader.GetInt32("UserNoTopics"),
                    UserNoReplies = reader.GetInt32("UserNoReplies"),
                    UserRep = reader.GetFloat("UserRep"),
                    UserIsBanned = reader.GetBoolean("UserIsBanned"),
                    UserIsAdmin = reader.GetBoolean("UserIsAdmin")
                });
            }

            return users;
        }
    }
}
