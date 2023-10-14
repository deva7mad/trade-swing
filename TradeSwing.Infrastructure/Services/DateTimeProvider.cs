using TradeSwing.Application.Common.Services;

namespace TradeSwing.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}