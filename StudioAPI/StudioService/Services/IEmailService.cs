namespace StudioService.Services
{
    public interface IEmailService
    {
        void SendEmail(string subject, string body, string to);
    }
}
