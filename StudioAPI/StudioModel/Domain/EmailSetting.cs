namespace StudioModel.Domain
{
    public class EmailSetting : Entity
    {
        public string? Email { get; set; }
        public string? AppPassword { get; set; }
        public int Port { get; set; }
    }
}
