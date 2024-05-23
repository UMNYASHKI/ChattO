using Application.Helpers;
using Application.Payment.DTOs;

namespace Application.Abstractions;

public interface IBillingService
{
    Task<Result<CreateOrderResponse>> CreateOrder(string value, string currency, string reference);

    Task<Result<CaptureOrderResponse>> CaptureOrder(string orderId);
}
