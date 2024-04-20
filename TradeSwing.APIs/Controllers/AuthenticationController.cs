using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TradeSwing.Contracts.Authentication;
using TradeSwing.Application.Authentication.Queries.Login;
using TradeSwing.Application.Authentication.Commands.Register;

namespace TradeSwing.APIs.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);
        var result = await mediator.Send(query);
        
        return result.Match(response => Ok(mapper.Map<AuthenticationResponse>(response)), Problem);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = mapper.Map<RegisterCommand>(request);

        var result = await mediator.Send(command);

        return result.Match(response => Ok(MapAuthResult(response)), Problem);
    }
}