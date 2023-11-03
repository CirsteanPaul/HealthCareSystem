namespace Healthcare.Application.Test.Queries;

public class GetTestResponse
{
    public IEnumerable<TestDto> Tests { get; set; }
}
public class TestDto
{
    public TestDto(Guid id, string name, int code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public int Code { get; set; }

    public string Name { get; set; }

    public Guid Id { get; set; }
}