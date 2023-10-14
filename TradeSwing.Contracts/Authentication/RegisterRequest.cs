namespace TradeSwing.Contracts.Authentication;

public record RegisterRequest(string FirstName, string LastName, string Email, string Mobile, string Password);