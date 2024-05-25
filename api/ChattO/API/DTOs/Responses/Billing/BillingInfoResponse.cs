using API.DTOs.Requests.Organization;
using Application.Helpers.Mappings;
using Application.Organizations.Commands;
using AutoMapper;
using Domain.Enums;
using Domain.Models;

namespace API.DTOs.Responses.Billing;

public class BillingInfoResponse : IMapWith<BillingInfo>
{
    public Guid Id { get; set; }

    public BillingType Type { get; set; }

    public double Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BillingInfo, BillingInfoResponse>()
           .ForMember(c => c.Id, opt => opt.MapFrom(r => r.Id))
           .ForMember(c => c.Price, opt => opt.MapFrom(r => r.Price))
           .ForMember(c => c.Type, opt => opt.MapFrom(r => r.Type));
    }
}
