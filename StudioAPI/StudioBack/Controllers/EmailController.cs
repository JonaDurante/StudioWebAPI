using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Email;
using StudioService.Services;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public IActionResult SendMail(EmailDto emailDto)
        {
            _emailService.SendEmail(emailDto.Subject, emailDto.Body, emailDto.To);
            return Ok();
        }
    }
}
