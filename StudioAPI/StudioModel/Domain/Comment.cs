using System.ComponentModel.DataAnnotations.Schema;

namespace StudioModel.Domain
{
    public class Comment : Entity
    {
        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        [ForeignKey("Video")]
        public Guid VideoId { get; set; }
        public DateTime CommentTime { get; set; } = DateTime.Now;
        public string CommentText { get; set; }

        public UserProfile Author { get; set; }
        public Video Video { get; set; }
    }
}
