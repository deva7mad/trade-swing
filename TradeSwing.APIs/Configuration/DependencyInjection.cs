using System.Reflection;

namespace TradeSwing.APIs.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var serviceInstallers = assemblies.SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();
        
        foreach (var installer in serviceInstallers) installer.Install(services, configuration);
        
        return services;
        
        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) && typeInfo is {IsInterface: false, IsAbstract: false};
    }
}