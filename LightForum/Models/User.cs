namespace LightForum.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserNickname { get; set; }
        public DateTime UserRegistrationDate { get; set; }
        public bool UserIsVerified { get; set; }
        public int UserNoTopics { get; set; }
        public int UserNoReplies { get; set; }
        public float UserRep { get; set; }
        public bool UserIsBanned { get; set; }
        public bool UserIsAdmin { get; set; }
    }
}
