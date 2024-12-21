using StudioDataAccess.Uow;
using StudioModel.Domain;
using System.Net;
using System.Net.Mail;

namespace StudioService.Services.Imp
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SendEmail(string subject, string body, string to)
        {
            EmailSetting? emailSetting = _unitOfWork.EmailSettingsRepository.GetAll().Result.FirstOrDefault();
            if (emailSetting is null)
            {
                throw new Exception("No se está cargando EmailSettingsRepository");
            }

            var fromEmail = emailSetting.Email;

            var message = new MailMessage();

            message.From = new MailAddress(fromEmail!);
            message.Subject = subject;
            message.To.Add(new MailAddress(to!));
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = emailSetting.Port,
                Credentials = new NetworkCredential(fromEmail, emailSetting.AppPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}
