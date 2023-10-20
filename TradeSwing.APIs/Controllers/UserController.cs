using System.Text.RegularExpressions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TradeSwing.Application.Services;
using TradeSwing.Contracts.Authentication;

namespace TradeSwing.APIs.Controllers;

[Route("users")]
public class UserController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public UserController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpGet("{username}")]
    public IActionResult LoginByUserName(string username)
    {
        var result = _authenticationService.Get(username);
        
        return result.Match(response => Ok(MapAuthResult(response)), Problem);
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.User.Mobile,
            result.Token
        );
    }
}