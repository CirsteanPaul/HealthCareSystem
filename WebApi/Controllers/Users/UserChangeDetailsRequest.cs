namespace WebApi.Controllers.Users;

public sealed class UserChangeDetailsRequest
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}