using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TradeSwing.Infrastructure.Services;
using TradeSwing.Application.Common.Services;
using TradeSwing.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using TradeSwing.Application.Persistence;
using TradeSwing.Infrastructure.Persistence;
using TradeSwing.Infrastructure.Data;
using TradeSwing.Application.Common.Interfaces.Authentication;

namespace TradeSwing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresSQLConnection")));
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}