using Healthcare.Domain.Entities;
using Healthcare.Domain.Shared.Results;
using Healthcare.Domain.ValueObjects;

namespace Healthcare.Application.Core.Abstractions.Data;

public interface IUserRepository : IAsyncRepository<User>
{
    Task<Result<User>> GetUserByCnp(Cnp cnp);
}