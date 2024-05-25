using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;

namespace API.DTOs.Responses.Billing;

public class BillingResponse : IMapWith<Domain.Models.Billing>
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid OrganizationId { get; set; }

    public BillingInfoResponse BillingInfo { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Billing, BillingResponse>()
           .ForMember(c => c.Id, opt => opt.MapFrom(r => r.Id))
           .ForMember(c => c.CreatedAt, opt => opt.MapFrom(r => r.CreatedAt))
           .ForMember(c => c.OrganizationId, opt => opt.MapFrom(r => r.OrganizationId))
           .ForMember(c => c.BillingInfo, opt => opt.MapFrom(r => r.BillingInfo));
    }
}
