using System.Net;

namespace TradeSwing.Application.Common.Errors;

public record struct DataDuplicationError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Invalid Registration Details";
}