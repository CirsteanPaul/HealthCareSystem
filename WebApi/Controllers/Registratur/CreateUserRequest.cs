namespace WebApi.Controllers.Registratur;

public sealed class CreateUserRequest
{
    public string? Cnp { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}