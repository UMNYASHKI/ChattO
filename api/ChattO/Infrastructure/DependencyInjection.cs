using Application.Abstractions;
using Domain.Models.Files;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Infrastructure.Services.Authentication;
using Infrastructure.Services.DataAccess;
using Infrastructure.Services.FilesStorage;
using Infrastructure.Services.Firebase;
using Infrastructure.Services.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection
            services, IConfiguration apiConfiguration)
    {
        services.Configure<JwtSettings>(apiConfiguration.GetSection(nameof(JwtSettings)));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, JwtService>();

        services.AddGoogleAuthentification(apiConfiguration)
            .AddJwtAuthentication(apiConfiguration);

        services.InitializeBackblaze(apiConfiguration);

        services.AddScoped<ICloudRepository, CloudRepository>();

        services.AddSingleton<ConnectionManager>();
        services.AddScoped<WebSocketService>();
        services.AddScoped<WebSocketHandler>();
        services.AddScoped<FirebaseMessagingHandler>();
        services.AddScoped<FirebaseMessagingService>();
        services.AddEmailSending(apiConfiguration);

        services.AddPaypalClient(apiConfiguration);

        return services;
    }
}
