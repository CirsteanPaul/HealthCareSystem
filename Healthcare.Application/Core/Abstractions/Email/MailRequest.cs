namespace Healthcare.Application.Core.Abstractions.Email;

public class MailRequest
{
    public MailRequest(string emailTo, string subject, string body)
    {
        EmailTo = emailTo;
        Subject = subject;
        Body = body;
    }

    public string EmailTo { get; }

    public string Subject { get; }

    public string Body { get; }
}