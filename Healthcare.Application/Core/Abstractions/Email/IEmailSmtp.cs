namespace Healthcare.Application.Core.Abstractions.Email;

public interface IEmailSmtp
{
    Task SendEmailAsync(MailRequest mailRequest);
}