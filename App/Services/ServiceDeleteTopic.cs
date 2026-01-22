namespace App.Services;
using App.Models;
using App.Models.Sets;
    public class ServiceDeleteTopic
    {
        public static int DeleteTopic(ForumDB _db, int id)
        {
            Topic topic = _db.topics.FirstOrDefault(t => t.id == id);
            _db.topics.Remove(topic);
            _db.SaveChanges();
            return id;
        }
    }
