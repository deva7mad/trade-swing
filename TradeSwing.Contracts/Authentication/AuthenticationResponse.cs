namespace TradeSwing.Contracts.Authentication;

public record AuthenticationResponse(Guid Id, string FirstName, string LastName, string Email, string Mobile, string Token);