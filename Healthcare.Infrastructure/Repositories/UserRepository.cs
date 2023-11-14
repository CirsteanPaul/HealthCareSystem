using Healthcare.Application.Core.Abstractions.Data;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;
using Healthcare.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Infrastructure.Repositories;

public sealed class UserRepository : AsyncRepository<User>, IUserRepository
{
    public UserRepository(HealthcareContext context) : base(context)
    {
    }
    
    public async Task<Result<User>> GetUserByCnp(Cnp cnp)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Cnp.Value == cnp.Value);
        
        return user ?? Result.Failure<User>(DomainErrors.General.EntityNotFoundError);
    }
}