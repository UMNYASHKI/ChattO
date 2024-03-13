using Application.Abstractions;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Infrastructure.Services.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection
            services)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        services.AddIdentity();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, JwtService>();

        return services;
    }
}
