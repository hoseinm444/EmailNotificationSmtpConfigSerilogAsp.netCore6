using SimpleEmailApp.Model;
namespace SimpleEmailApp.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailMessage mailRequest);

    }
}
