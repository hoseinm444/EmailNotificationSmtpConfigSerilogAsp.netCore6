
using Microsoft.AspNetCore.Mvc;
using SimpleEmailApp.CorrelationService;
using System.Text.Json;

namespace SimpleEmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly ILogger<MailController> _logger;
        private readonly ICorrelationIdGenerator _correlationIdGenerator;
        public MailController(IMailService mailService, ILogger<MailController> logger,
            ICorrelationIdGenerator correlationIdGenerator)
        {
            _mailService = mailService;
            _logger = logger;
            _correlationIdGenerator = correlationIdGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail([FromForm] EmailMessage req)
        {
            _logger.LogInformation("..................... Sending Email Function Start.............");
            try
            {
                await _mailService.SendEmailAsync(req);
                _logger.LogInformation("CorrelationId {correlationId}: ",
           _correlationIdGenerator.Get());

                  //_logger.LogInformation($" Email  {},{req.Reciver},{req.Subject},{req.Body} is send.");
                return Ok($"Email sended to {req.Reciver} sucessfully.");

            }
            catch (Exception)
            {
                return NotFound();
     //           _logger.LogError("We have error in sending Email.");
            }
            

        }



        //public IActionResult SendEmail(string subject , string body , string sender)
        //{
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse("anivargudeov@gmail.com"));
        //    email.To.Add(MailboxAddress.Parse("anivargudeov@gmail.com"));
        //    email.Subject = subject;
        //    email.Body =new TextPart(TextFormat.Plain) { Text= body };

        //    using var smtp = new SmtpClient();
        //    smtp.Connect("smtp.gmail.com", 587,SecureSocketOptions.StartTls);
        //    smtp.Authenticate("anivargudeov@gmail.com", "fsobaslupqqlmhtv");
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //    return Ok();
        //}

    }
}
