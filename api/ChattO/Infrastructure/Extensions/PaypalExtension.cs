using Application.Abstractions;
using Infrastructure.Services.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class PaypalExtension
{
    public static void AddPaypalClient(this IServiceCollection services, IConfiguration configuration)
    {
        var paypalSection = configuration.GetSection("PayPalOptions");
        services.AddSingleton<IBillingService>(x => new PaypalService(paypalSection.GetValue<string>("ClientId"), paypalSection.GetValue<string>("ClientSecret")));
    }
}
