namespace TradeSwing.APIs.Configuration;

public interface IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration);
}