using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleEmailApp.ConfgureSetting;

namespace SimpleEmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMTPConfigurationController : ControllerBase
    {

         private readonly IWritableOptionsMail<AppSetting> _writableMail;
        private readonly ILogger<SMTPConfigurationController> _logger;
        //IWritableOptionsMail<AppSetting> writableMail
        public SMTPConfigurationController(IWritableOptionsMail<AppSetting> writableMail,
            ILogger<SMTPConfigurationController> logger)
        {
            _writableMail = writableMail;
            _logger = logger;
        }


        [HttpPost]
        public IActionResult ConfigureSettings(string SenderMail, string password, string host, int port)
        {
            var setting = new AppSetting()
            {
                Mail = SenderMail,
                Password = password,
                Host = host,
                Port = port
            };


            if (setting == null)
            {
                return BadRequest("you should Enter configuration for SMTP Server");
            }
            try
            {
                _writableMail.Update(opt =>
                {
                    opt.Mail = setting.Mail;
                    opt.Password = setting.Password;
                    opt.Host = setting.Host;
                    opt.Port = setting.Port;
                });
                _logger.LogInformation("New SMTP Configuration {setting} is created. ",setting);
            }
            catch(Exception)
            {
                _logger.LogError("SMTP configuration has error.");
                throw;
            }
           

            // if (_settings != null)
            return Ok("configuration is added into setting .");
        }

    }
}
