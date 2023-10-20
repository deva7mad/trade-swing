using TradeSwing.APIs.Common.Errors;
using TradeSwing.APIs.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace TradeSwing.APIs;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // Add services to the container.
        // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, TradeSwingProblemDetailsFactory>();
        services.AddMappings();
        
            
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        AddSwaggerSecurityConfig();
        
        return services;

        void AddSwaggerSecurityConfig()
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo {Title = "TradeSwing Web APIs.", Version = "v1"});
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}