using ErrorOr;
using MediatR;
using TradeSwing.Application.Persistence;
using TradeSwing.Application.Services;
using TradeSwing.Domain.Common.Errors;
using TradeSwing.Application.Common.Interfaces.Authentication;

namespace TradeSwing.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var user = _userRepository.GetUserByMobile(request.Mobile);
        
        if (user is null)
            return Errors.User.UserNotFound;

        return user.Password != request.Password ? Errors.Validation.InvalidCredentials : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }
}