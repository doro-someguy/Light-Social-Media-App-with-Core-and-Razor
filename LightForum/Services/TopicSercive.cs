using MySql.Data.MySqlClient;
using LightForum.Models;

namespace LightForum.Services
{
    public class TopicService
    {
        private readonly string? _connectionString;

        public TopicService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Topic> GetAllTopics()
        {
            List<Topic> topics = new List<Topic>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM topic";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                topics.Add(new Topic
                {
                    TopicId = reader.GetInt32("TopicId"),
                    TopicAuthor = reader.IsDBNull(reader.GetOrdinal("TopicAuthor")) 
                                    ? null 
                                    : reader.GetString("TopicAuthor"),
                    TopicTitle = reader.GetString("TopicTitle"),
                    TopicContent = reader.GetString("TopicContent"),
                    TopicCreationDate = reader.GetDateTime("TopicCreationDate"),
                    TopicLikes = reader.GetInt32("TopicLikes"),
                    TopicNoReplies = reader.GetInt32("TopicNoReplies")
                });
            }

            return topics;
        }
    }
}
