using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Comment
{
    public class CommentDto
    {
        public Guid VideoId { get; set; }
        public Guid AuthorId { get; set; }
        [StringLength(300)]
        public string CommentText { get; set; }
    }
}
