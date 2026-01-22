namespace App.Services;

using App.Models;
using App.Models.Sets;

public static class ServiceViewTopic
{
    public static PostAndComments GetTopicAndCommentsAndPost(ForumDB _db, IFormCollection form, int id, string author)
    {
        var topic = _db.topics.FirstOrDefault(t => t.id == id);
        var postAndComments = new PostAndComments { topic = topic };
        string content = form["content"];
        var comment = new Comment
        {
            author = author,
            content = content,
            parentTopic = id,
            creationDate = DateTime.Now
        };
        _db.comments.Add(comment);
        _db.SaveChanges();
        var comments = _db.comments.Where(c => c.parentTopic == id).ToList();
        postAndComments.comments = comments;
        return postAndComments;
    }
    public static PostAndComments GetTopicAndComments(ForumDB _db, int id)
    {
        var topic = _db.topics.FirstOrDefault(t => t.id == id);
        var postAndComments = new PostAndComments { topic = topic };
        var comments = _db.comments.Where(c => c.parentTopic == id).ToList();
        postAndComments.comments = comments;
        return postAndComments;
    }
}
