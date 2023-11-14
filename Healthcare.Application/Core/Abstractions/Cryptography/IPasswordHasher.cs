using Healthcare.Domain.Shared.Results;

namespace Healthcare.Application.Core.Abstractions.Cryptography;

public interface IPasswordHasher
{
    Result<string> HashPassword(string password);
}