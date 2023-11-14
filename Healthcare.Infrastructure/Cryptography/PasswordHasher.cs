using DevOne.Security.Cryptography.BCrypt;
using Healthcare.Application.Core.Abstractions.Cryptography;
using Healthcare.Domain.Errors;
using Healthcare.Domain.Shared.Results;

namespace Healthcare.Infrastructure.Cryptography;

public sealed class PasswordHasher : IPasswordHasher
{
    // We use a static Salt not the best option.
    private const string Salt = "$2a$05$S7/307HHJnOk60ax5Rc6ju";
    
    public Result<string> HashPassword(string password)
    {
        try
        {
            var hashedPassword = BCryptHelper.HashPassword(password, Salt)!;
            
            return hashedPassword;
        }
        catch(Exception)
        {
            // TODO: Should log the error.
            return Result.Failure<string>(DomainErrors.General.PasswordCannotBeHashedError);
        }
    }
}