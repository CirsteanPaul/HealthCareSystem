namespace Healthcare.Application.Core.Abstractions.Authentication;

public interface IUserIdentityProvider
{ 
    string UserId { get; }
}