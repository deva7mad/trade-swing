using System.Net;

namespace TradeSwing.Application.Common.Errors;

public class DuplicateRegistrationDataException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Invalid Registration Details";
}