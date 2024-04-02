using Application.Abstractions;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Infrastructure.Services.Authentication;
using Infrastructure.Services.DataAccess;
using Infrastructure.Services.FilesStorage;
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

        services.AddJwtAuthentication(apiConfiguration);

        services.InitializeBackblaze(apiConfiguration);

        services.AddScoped<ICloudRepository, CloudRepository>();

        return services;
    }
}
