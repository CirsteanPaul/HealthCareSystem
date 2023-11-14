namespace WebApi.Controllers.Users;

public class UserLoginRequest
{
    public string? Cnp { get; set; }
    public string? Password { get; set; }
}