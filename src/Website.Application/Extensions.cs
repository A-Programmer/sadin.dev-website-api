using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Website.Application.ContactUsMessages.Commands.CreateMessage;
using Website.Application.ContactUsMessages.Queries.GetMessageById;

namespace Website.Application;

public static class Extensions
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(CreateContactUsMessageCommand));
        services.AddMediatR(typeof(GetMessageByIdQuery));
        return services;
    }

    public static IApplicationBuilder UseApplicationLayer(this IApplicationBuilder app)
    {
        return app;
    }
}