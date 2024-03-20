using Healthcare.Domain.Entities;

namespace Healthcare.Application.Core.Notifications;

public sealed class WelcomeEmail
{
    public WelcomeEmail(string emailTo, UserPermission userPermission)
    {
        EmailTo = emailTo;
        UserPermission = userPermission;
    }
    
    public string EmailTo { get; }
    public UserPermission UserPermission { get; }
}