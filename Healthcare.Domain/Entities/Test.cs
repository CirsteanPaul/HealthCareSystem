namespace Healthcare.Domain.Entities;

public class Test
{
    public Test(Guid id, string name, int code)
    {
        Id = id;
        Name = name;
        Code = code;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Code { get; private set; }
}