using ErrorOr;
using FluentResults;
using OneOf;
using IError = TradeSwing.Application.Common.Errors.IError;

namespace TradeSwing.Application.Services;

public interface IAuthenticationService
{
    Result<AuthenticationResult> Login(string mobile, string password);
    OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email,
        string mobile, string password);

    ErrorOr<AuthenticationResult> Get(string username);
}