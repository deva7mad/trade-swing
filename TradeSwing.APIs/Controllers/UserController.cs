using Microsoft.AspNetCore.Mvc;
using TradeSwing.Application.Services;

namespace TradeSwing.APIs.Controllers;

[Route("api/v1/[controller]s")]
public class UserController(IAuthenticationService authenticationService) : ApiController
{
    [HttpGet("{username}")]
    public IActionResult LoginByUserName(string username)
    {
        var result = authenticationService.Get(username);
        
        return result.Match(response => Ok(MapAuthResult(response)), Problem);
    }
}