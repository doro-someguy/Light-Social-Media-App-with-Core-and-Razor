namespace App.Services;

using App.Models;
using App.Models.Sets;
public class ServiceEditComment
{
    public static int FinishEditComment(ForumDB _db, IFormCollection form, int id)
    {
        String editedCommentContent = form["content"];
        Comment comment = _db.comments.FirstOrDefault(t => t.id == id);
        comment.content = editedCommentContent;
        _db.SaveChanges();
        return comment.parentTopic;
    }
    public static Comment ReturnComment(ForumDB _db, int id)
    {
        Comment comment = _db.comments.FirstOrDefault(t => t.id == id);
        _db.SaveChanges();
        return comment;
    }
}
