using ErrorOr;
using FluentResults;
using OneOf;
using TradeSwing.Application.Common.Errors;
using TradeSwing.Application.Common.Interfaces.Authentication;
using TradeSwing.Application.Persistence;
using TradeSwing.Domain.Common.Errors;
using TradeSwing.Domain.Entities;
using IError = TradeSwing.Application.Common.Errors.IError;
using Result = FluentResults.Result;

namespace TradeSwing.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public Result<AuthenticationResult> Login(string mobile, string password)
    {
        var user = _userRepository.GetUserByMobile(mobile);
        
        if (user is null)
            return Result.Fail<AuthenticationResult>(new FluentResultError());

        return user.Password != password ? Result.Fail<AuthenticationResult>(new FluentResultError()) : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }

    public OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string mobile, string password)
    {
        if (_userRepository.GetUserEmail(email) is not null || _userRepository.GetUserByMobile(mobile) is not null)
            return new DataDuplicationError();

        var user = new UserEntity
        {
            FirstName = firstName, 
            LastName = lastName, 
            Mobile = mobile, 
            Email = email, 
            Password = password
        };
        
        _userRepository.AddUser(user);
        
        return new AuthenticationResult(user,  _jwtTokenGenerator.GenerateToken(user));
    }

    public ErrorOr<AuthenticationResult> Get(string username)
    {
        if (username.Contains('_'))
            return Errors.Validation.InvalidFormat;

        if (_userRepository.GetUserEmail(username) is not null || _userRepository.GetUserByMobile(username) is not null)
            return Errors.User.DuplicateData;

        var user = new UserEntity
        {
            Email = "email",
            Mobile = "mobile",
            FirstName = "first",
            LastName = "Last",
            Password = "string"
        };

        return new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }
}