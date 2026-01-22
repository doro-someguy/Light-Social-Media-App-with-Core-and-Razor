namespace App.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string author { get; set; }
        public DateTime creationDate { get; set; }
        public int parentTopic {  get; set; }
        public string content { get; set; }
    }
}
