namespace WebApi.Controllers.Users;

public class UserChangePasswordRequest
{
    public Guid UserId { get; set; }
    public string? NewPassword { get; set; }
}