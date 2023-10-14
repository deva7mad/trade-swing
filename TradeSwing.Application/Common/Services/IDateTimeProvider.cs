namespace TradeSwing.Application.Common.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}