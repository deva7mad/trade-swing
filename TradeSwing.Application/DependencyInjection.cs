using System.Reflection;
using TradeSwing.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TradeSwing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}