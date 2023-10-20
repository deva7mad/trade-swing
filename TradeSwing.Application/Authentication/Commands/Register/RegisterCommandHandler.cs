using ErrorOr;
using MediatR;
using TradeSwing.Domain.Entities;
using TradeSwing.Application.Services;
using TradeSwing.Domain.Common.Errors;
using TradeSwing.Application.Persistence;
using TradeSwing.Application.Common.Interfaces.Authentication;

namespace TradeSwing.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserEmail(request.Email) is not null || _userRepository.GetUserByMobile(request.Mobile) is not null)
            return Errors.User.DuplicateData;

        var user = new UserEntity
        {
            FirstName = request.FirstName, 
            LastName = request.LastName, 
            Mobile = request.Mobile, 
            Email = request.Email, 
            Password = request.Password
        };
        
        _userRepository.AddUser(user);
        
        return new AuthenticationResult(user,  _jwtTokenGenerator.GenerateToken(user));
    }
}