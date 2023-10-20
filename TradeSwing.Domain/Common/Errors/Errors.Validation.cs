using ErrorOr;

namespace TradeSwing.Domain.Common.Errors;

public static partial class Errors
{
    public static class Validation
    {
        public static Error InvalidFormat = Error.Validation(code: "User.Input.Format.Error", description: "Invalid Inputs.");
        public static Error InvalidCredentials = Error.Validation(code: "User.Input.Credentials.Error", description: "Invalid Credentials.");
    }
}