using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;

namespace API.DTOs.Responses.Billing;

public class BillingInfoResponse : IMapWith<BillingInfo>
{
    public Guid Id { get; set; }

    public BillingType Type { get; set; }

    public double Price { get; set; }
}
