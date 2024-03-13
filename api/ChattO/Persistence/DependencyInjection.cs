using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataAccess;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
    {
        services.AddDbContext<ChattoDbContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IChattoDbContext>(provider =>
                provider.GetService<ChattoDbContext>());

        return services;
    }
}
