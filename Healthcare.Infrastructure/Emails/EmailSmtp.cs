using Healthcare.Application.Core.Abstractions.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Healthcare.Infrastructure.Emails;

public sealed class EmailSmtp : IEmailSmtp
{
    private readonly MailSettings _mailSettings;

    public EmailSmtp(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage
        {
            Subject = mailRequest.Subject,
            Body = new TextPart(TextFormat.Text)
            {
                Text = mailRequest.Body
            }
        };
        
        email.From.Add(new MailboxAddress(_mailSettings.SenderDisplayName, _mailSettings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(mailRequest.EmailTo));

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);

        await smtpClient.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.SmtpPassword);

        await smtpClient.SendAsync(email);

        await smtpClient.DisconnectAsync(true);
    }
}