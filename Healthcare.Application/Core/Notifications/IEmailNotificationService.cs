namespace Healthcare.Application.Core.Notifications;

public interface IEmailNotificationService
{
    Task SendWelcomeEmail(WelcomeEmail email);
}