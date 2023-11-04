using Healthcare.Application.Core.Abstractions.Messaging;

namespace Healthcare.Application.Test.Commands;

public sealed record CreateTestRequestCommand(string Name, int Code) : ICommand<CreateTestResponse>;