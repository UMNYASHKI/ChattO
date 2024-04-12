using Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class BackBlazeSetup
{
    public static void InitializeBackblaze(this IServiceCollection services, IConfiguration configuration)
    {
        var backblazeSection = configuration.GetSection("BackBlaze");

        var appKey = backblazeSection.GetSection("Secrets").GetValue<string>("ApplicationKey");
        var keyId = backblazeSection.GetSection("Secrets").GetValue<string>("KeyId");

        services.AddBackblazeAgent(options =>
        {
            options.ApplicationKey = appKey;
            options.KeyId = keyId;
        });

        services.Configure<BucketSettings>(backblazeSection.GetSection("Bucket"));

        services.AddMemoryCache();
    }
}
