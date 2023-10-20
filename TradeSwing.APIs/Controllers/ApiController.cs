using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TradeSwing.APIs.Common.Http;
using TradeSwing.Application.Services;
using TradeSwing.Contracts.Authentication;

namespace TradeSwing.APIs.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        
        var error = errors.First();
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
        
        return Problem(statusCode: statusCode, title: error.Description);
    }
    
    protected static AuthenticationResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.UserEntity.Id,
            result.UserEntity.FirstName,
            result.UserEntity.LastName,
            result.UserEntity.Email,
            result.UserEntity.Mobile,
            result.Token
        );
    }
}