using Healthcare.Application.Core.Abstractions.Messaging;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Features.Admin.CreateEmployee;

public sealed record CreateEmployeeCommand(string Cnp, string PhoneNumber, 
    string Email, UserPermission UserPermission) : ICommand;