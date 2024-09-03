namespace StudioModel.Domain
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsVisible { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

    }
}
