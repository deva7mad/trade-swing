using ErrorOr;

namespace TradeSwing.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateData = Error.Conflict(code: "User.Data.Duplication.Error", description: "User Exits Before.");
        public static Error UserNotFound = Error.Conflict(code: "User.Data.NotFound.Error", description: "User Doesn't Exits.");
    }
}