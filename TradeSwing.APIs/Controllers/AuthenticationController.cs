using Microsoft.AspNetCore.Mvc;
using TradeSwing.Application.Services;
using TradeSwing.Contracts.Authentication;

namespace TradeSwing.APIs.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authenticationService.Login(request.Mobile, request.Password);
        return Ok(new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.User.Mobile, result.Token));
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authenticationService.Register(request.FirstName, request.LastName,
            request.Email, request.Mobile, request.Password);
        
        return Ok(new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.User.Mobile, result.Token));

    }
}