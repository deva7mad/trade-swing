using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradeSwing.Application.Common.Errors;

namespace TradeSwing.APIs.Controllers;

[ApiExplorerSettings(IgnoreApi=true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (statusCode, errorMessage) = exception switch
        {
            IServiceException serviceException => ((int) serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
        };
        return Problem(statusCode: statusCode, title: errorMessage);
    }
}