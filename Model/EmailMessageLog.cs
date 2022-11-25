namespace SimpleEmailApp.Model
{
    public class EmailMessageLog
    {
        public int Id { get; set; }
        public string Reciver { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } 

        public EmailMessageLog(int id,string recive,string send,string subject, string body)
        {
            Id = id;
            Reciver = recive;
            Sender = send;
            Subject = subject;
            Body = body;
        }
    }
}
