using TradeSwing.Domain.Entities;

namespace TradeSwing.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}