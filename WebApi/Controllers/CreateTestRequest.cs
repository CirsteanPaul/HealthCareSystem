namespace WebApi.Controllers;

public sealed class CreateTestRequest
{
    public string? Name { get; set; }

    public int? Code { get; set; }
}