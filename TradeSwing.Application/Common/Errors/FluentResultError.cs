namespace TradeSwing.Application.Common.Errors;

public class FluentResultError : FluentResults.IError
{
    public string Message => "Invalid Credentials.";
    public Dictionary<string, object> Metadata { get; }
    public List<FluentResults.IError> Reasons { get; }
}