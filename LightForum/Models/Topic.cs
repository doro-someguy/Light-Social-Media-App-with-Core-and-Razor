namespace LightForum.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string? TopicAuthor { get; set; }
        public string TopicTitle { get; set; } = default!;
        public string TopicContent { get; set; } = default!;
        public DateTime TopicCreationDate { get; set; }
        public int TopicLikes { get; set; }
        public int TopicNoReplies { get; set;}

    }
}