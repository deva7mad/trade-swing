using TradeSwing.Application.Common.Interfaces.Authentication;
using TradeSwing.Application.Persistence;
using TradeSwing.Domain.Entities;

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
    
    public AuthenticationResult Login(string mobile, string password)
    {
        var user = _userRepository.GetUserByMobile(mobile);
        if (user is null)
            throw new Exception("Invalid Credentials.");

        if (user.Password != password)
            throw new Exception("Invalid Credentials.");

        return new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string mobile, string password)
    {
        if (_userRepository.GetUserEmail(email) is not null)
        {
            throw new Exception("User with given email already exits.");
        }
        if (_userRepository.GetUserByMobile(mobile) is not null)
        {
            throw new Exception("User with given mobile already exits.");
        }

        var user = new User
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
}