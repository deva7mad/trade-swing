using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TradeSwing.APIs.Controllers;

[ApiExplorerSettings(IgnoreApi=true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(detail: exception?.Message, statusCode: 400);
    }
}