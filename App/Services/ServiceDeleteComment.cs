namespace App.Services;

using App.Models;
using App.Models.Sets;
public class ServiceDeleteComment
{
    public static List<int> DeleteComment(ForumDB _db, int id)
    {
        Comment comment = _db.comments.FirstOrDefault(t => t.id == id);
        _db.comments.Remove(comment);
        _db.SaveChanges();
        List<int> ids = new List<int>();
        ids.Add(id);
        ids.Add(comment.parentTopic);
        return ids;
    }
}
