using BackgroundJobs.Abstractions.Messaging;
using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Application.Core.Notifications;
using Healthcare.Application.Events.CreatedUser;
using Microsoft.Extensions.Logging;

namespace BackgroundJobs.IntegrationEvents.Users.UserCreated;

public sealed class SendWelcomeEmailCreatedIntegrationEventHandler : IIntegrationEventHandler<CreatedUserIntegrationEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SendWelcomeEmailCreatedIntegrationEventHandler> _logger;
    private readonly  IEmailNotificationService _emailNotificationService;

    public SendWelcomeEmailCreatedIntegrationEventHandler(IUserRepository userRepository, 
        IEmailNotificationService emailNotificationService, ILogger<SendWelcomeEmailCreatedIntegrationEventHandler> logger)
    {
        _userRepository = userRepository;
        _emailNotificationService = emailNotificationService;
        _logger = logger;
    }

    public async Task Handle(CreatedUserIntegrationEvent notification, CancellationToken cancellationToken)
    {
        
        var userResult = await _userRepository.FindByIdAsync(notification.UserId);

        if (userResult.IsFailure)
        {
            // TODO: Handle errors ?
            return;
        }

        var user = userResult.Value;
        
        _logger.LogInformation("An email is being sent to user with email {Email} and permission {UserPermission}",
            user.Email.Value, user.UserPermission);
        
        await _emailNotificationService.SendWelcomeEmail(new WelcomeEmail(user.Email.Value, user.UserPermission));
    }
}