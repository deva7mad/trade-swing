using TradeSwing.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TradeSwing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}