using Healthcare.Application.Core.Abstractions.Email;
using Healthcare.Application.Core.Notifications;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure.Notifications;

public sealed class EmailNotificationService : IEmailNotificationService
{
    private readonly IEmailSmtp _emailSmtp;

    public EmailNotificationService(IEmailSmtp emailSmtp)
    {
        _emailSmtp = emailSmtp;
    }

    public async Task SendWelcomeEmail(WelcomeEmail email)
    {
        var mailRequest = new MailRequest(
            email.EmailTo,
            "Welcome to Healthcare",
            "You're account has been created" +
            Environment.NewLine +
            $"You're account is of type {ConvertUserPermissionToString(email.UserPermission)}"
        );

        await _emailSmtp.SendEmailAsync(mailRequest);
    }

    private string ConvertUserPermissionToString(UserPermission userPermission) => userPermission switch
    {
        UserPermission.Admin => nameof(UserPermission.Admin),
        UserPermission.Patient => nameof(UserPermission.Patient),
        UserPermission.Pharmacist => nameof(UserPermission.Pharmacist),
        UserPermission.Doctor => nameof(UserPermission.Doctor),
        UserPermission.Registratur => nameof(UserPermission.Registratur),
        UserPermission.Unknown => "Something went wrong please call the administrator"
    };
}