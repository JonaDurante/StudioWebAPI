using System.ComponentModel.DataAnnotations.Schema;

namespace StudioModel.Domain
{
    public class Comment : Entity
    {
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        [ForeignKey("Video")]
        public Guid VideoId { get; set; }
        public DateTime CommentTime { get; set; } = DateTime.Now;
        public string CommentText { get; set; }

        public UserApp Author { get; set; }
        public Video Video { get; set; }
    }
}
