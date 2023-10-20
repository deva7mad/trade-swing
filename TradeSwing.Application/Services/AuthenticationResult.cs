using TradeSwing.Domain.Entities;

namespace TradeSwing.Application.Services;

public record AuthenticationResult(UserEntity UserEntity, string Token);