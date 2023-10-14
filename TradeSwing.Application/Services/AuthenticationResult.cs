using TradeSwing.Domain.Entities;

namespace TradeSwing.Application.Services;

public record AuthenticationResult(User User, string Token);