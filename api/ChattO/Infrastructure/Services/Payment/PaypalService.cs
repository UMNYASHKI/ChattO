using Application.Abstractions;
using Application.Helpers;
using Application.Payment.DTOs;
using Infrastructure.Helpers;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Payment;

public sealed class PaypalService : IBillingService
{
    public string ClientId { get; }
    public string ClientSecret { get; }

    public PaypalService(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }

    private async Task<Result<AuthResponse>> Authenticate()
    {
        var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));

        var content = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials")
            };

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"{PayPalConstants.BaseUrl}/v1/oauth2/token"),
            Method = HttpMethod.Post,
            Headers =
                {
                    { "Authorization", $"Basic {auth}" }
                },
            Content = new FormUrlEncodedContent(content)
        };

        try
        {
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.SendAsync(request);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<AuthResponse>(jsonResponse);

            return Result.Success(response);
        }
        catch (Exception e)
        {
            return Result.Failure<AuthResponse>(e.Message);
        }
    }

    public async Task<Result<CreateOrderResponse>> CreateOrder(string value, string currency, string reference)
    {
        var authResult = await Authenticate();
        if (!authResult.IsSuccessful)
        {
            return Result.Failure<CreateOrderResponse>(authResult.Message);
        }

        var request = new CreateOrderRequest
        {
            intent = "CAPTURE",
            purchase_units = new List<PurchaseUnit>
                {
                    new()
                    {
                        reference_id = reference,
                        amount = new Amount
                        {
                            currency_code = currency,
                            value = value
                        }
                    }
                }
        };

        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {authResult.Data.access_token}");

        try
        {
            var httpResponse = await httpClient.PostAsJsonAsync($"{PayPalConstants.BaseUrl}/v2/checkout/orders", request);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);

            return Result.Success(response);
        }
        catch (Exception e)
        {
            return Result.Failure<CreateOrderResponse>(e.Message);
        }
    }

    public async Task<Result<CaptureOrderResponse>> CaptureOrder(string orderId)
    {
        var authResult = await Authenticate();
        if (!authResult.IsSuccessful)
        {
            return Result.Failure<CaptureOrderResponse>(authResult.Message);
        }

        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {authResult.Data.access_token}");

        var httpContent = new StringContent("", Encoding.Default, "application/json");

        try
        {
            var httpResponse = await httpClient.PostAsync($"{PayPalConstants.BaseUrl}/v2/checkout/orders/{orderId}/capture", httpContent);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<CaptureOrderResponse>(jsonResponse);

            return Result.Success(response);
        }
        catch (Exception e)
        {
            return Result.Failure<CaptureOrderResponse>(e.Message);
        }
    }
}

