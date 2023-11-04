namespace Healthcare.Application.Core.Abstractions.Authentication;

public interface IJwtProvider
{
    string Create(string user);
}