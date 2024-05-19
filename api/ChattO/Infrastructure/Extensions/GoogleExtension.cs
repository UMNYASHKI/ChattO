using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class GoogleExtension
{
    public static IServiceCollection AddGoogleAuthentification(this IServiceCollection services, IConfiguration configuration)
    {
        var clientId = configuration.GetSection("GoogleAuthSettings:ClientId").Get<string>();
        var clientSecret = configuration.GetSection("GoogleAuthSettings:ClientSecret").Get<string>();

        services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
            });

        return services;
    }
}
