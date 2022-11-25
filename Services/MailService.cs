using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace SimpleEmailApp.Services
{
    public class MailService : IMailService
    {
        private readonly AppSetting _mailSetting;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<AppSetting> mailSetting,ILogger<MailService> logger)
        {
            _mailSetting = mailSetting.Value;
            _logger = logger;   
        }

        public async Task SendEmailAsync(EmailMessage mailRequest)
        {
            var email = new MimeMessage();
            try {
                
                email.Sender = MailboxAddress.Parse(_mailSetting.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.Reciver));

                email.Subject = mailRequest.Subject;
                email.Body = new TextPart(TextFormat.Plain) { Text = mailRequest.Body };


                _logger.LogInformation("Email {@mailRequest} is created at {now}.", mailRequest, DateTime.Now);
            }
            catch(Exception )
            {
                _logger.LogError("Email is not sended.");
                throw;
            }
            
            try{
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSetting.Mail, _mailSetting.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch(Exception)
            {
                _logger.LogError("Error In Sending email is occured");
                throw;
            }
        }

    }
}
