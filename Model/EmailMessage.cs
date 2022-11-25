
using Serilog;
namespace SimpleEmailApp.Model
{ 
    public class EmailMessage
    {     
        public string Reciver { get; set; } = string.Empty;
       // public string Sender { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        //  public List<IFormFile> Attachments { get; set; } 
    }
}
