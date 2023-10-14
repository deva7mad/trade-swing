using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TradeSwing.APIs.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetails = new ProblemDetails
        {
            Title = "An error occured while processing your request.",
            Detail = exception.Message,
            Status = (int) HttpStatusCode.InternalServerError,
            Instance = context.HttpContext.Request.Path
        };

        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}