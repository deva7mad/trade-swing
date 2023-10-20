using Microsoft.AspNetCore.Mvc;
using TradeSwing.Application.Services;

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
}