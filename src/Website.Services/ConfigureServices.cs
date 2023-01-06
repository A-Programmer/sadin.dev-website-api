using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Website.Services.AuthServices;

namespace Website.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        //Register Services
        
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IApplicationBuilder UseServicesLayer(this IApplicationBuilder app)
    {
        //Configure
        return app;
    }
}