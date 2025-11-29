namespace LightForum.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }               
        public int ReplyBelongsTo { get; set; }        
        public string? ReplyAuthor { get; set; }      
        public string ReplyContent { get; set; } = ""; 
        public DateTime ReplyCreationDate { get; set; }
        public int ReplyLikes { get; set; }
    }
}
