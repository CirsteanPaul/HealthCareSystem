using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Shared;

namespace Healthcare.Application.Test.Commands;

public sealed class CreateTestRequestCommand : ICommand<Result>
{
    public CreateTestRequestCommand(string name, int code)
    {
        Name = name;
        Code = code;
    }

    public string Name { get; }

    public int Code { get; }
}