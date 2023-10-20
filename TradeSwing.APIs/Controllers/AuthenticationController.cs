using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TradeSwing.Contracts.Authentication;
using TradeSwing.Application.Authentication.Queries.Login;
using TradeSwing.Application.Authentication.Commands.Register;

namespace TradeSwing.APIs.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var result = await _mediator.Send(query);
        
        return result.Match(response => Ok(_mapper.Map<AuthenticationResponse>(response)), Problem);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var result = await _mediator.Send(command);

        return result.Match(response => Ok(MapAuthResult(response)), Problem);
    }
}