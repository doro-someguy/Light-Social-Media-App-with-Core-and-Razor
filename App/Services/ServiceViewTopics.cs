namespace App.Services;

using App.Models;

public static class ServiceViewTopics
{
    public static List<Topic> GetTopics(ForumDB _db)
    {
        var topics = _db.topics.ToList();
        return topics;
    }
}
