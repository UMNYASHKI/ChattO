using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Billing;

public class CreateBillingInfoRequest: IMapWith<Domain.Models.BillingInfo>
{
    [Required]
    public BillingType Type { get; set; }

    [Required]
    public double Price { get; set; }
}
