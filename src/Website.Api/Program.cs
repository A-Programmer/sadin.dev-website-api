using Microsoft.AspNetCore.Server.Kestrel.Core;
using Website.Application;
using Website.Common;
using Website.Infrastructure;
using Website.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration;

if (builder.Environment.IsProduction())
{
    Configuration = Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
}
else
{
    Configuration = Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Development.json")
        .Build();
}
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
        options.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.ListenAnyIP(2002, options =>
//     {
//         options.Protocols = HttpProtocols.Http1;
//         options. UseHttps("server-cert","123456");
//     });
// });
builder.Services.AddApplicationLayerServices();
builder.Services.AddInfrastructureLayerServices(Configuration);
builder.Services.AddServicesLayer();
builder.Services.AddSharedServices(Configuration);

var app = builder.Build();
app.UseCors("AllowOrigin");
app.UseSharedServices();
app.UseApplicationLayer();
app.UseInfrastructureLayer();
app.UseServicesLayer();

app.Run();
