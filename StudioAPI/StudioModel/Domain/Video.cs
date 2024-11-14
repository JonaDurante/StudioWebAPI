namespace StudioModel.Domain
{
    public class Video : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public bool IsVisible { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

        public ICollection<Comment?> Comments { get; set; } = new HashSet<Comment?>();
    }
}
