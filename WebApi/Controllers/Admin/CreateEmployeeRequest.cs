namespace WebApi.Controllers.Admin;

public class CreateEmployeeRequest
{
    public string? Cnp { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public short UserPermission { get; set; }
}