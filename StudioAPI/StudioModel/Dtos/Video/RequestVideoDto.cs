using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Video
{
    public class RequestVideoDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }
        public bool IsVisible { get; set; }

        [DataType(DataType.Text)]
        public string AuthorId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Text)]
        public string Url { get; set; }

        [DataType(DataType.Text)]
        public string ThumbnailUrl { get; set; }
    }
}
