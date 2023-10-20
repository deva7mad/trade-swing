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
        return result.IsSuccess
            ? Ok(new AuthenticationResponse(result.Value.User.Id, result.Value.User.FirstName,
                result.Value.User.LastName, result.Value.User.Email, result.Value.User.Mobile, result.Value.Token))
            : Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Errors.First().Message);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authenticationService.Register(request.FirstName, request.LastName,
            request.Email, request.Mobile, request.Password);

        return result.Match(
            response => Ok(new AuthenticationResponse(response.User.Id, response.User.FirstName, response.User.LastName, response.User.Email, response.User.Mobile, response.Token)),
            error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage)
                );
    }
}