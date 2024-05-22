using API.DTOs.Paging;
using API.DTOs.Sorting;
using Application.Helpers.Mappings;
using Application.Payment.Queries;
using AutoMapper;

namespace API.DTOs.Requests.Billing;

public class GetOrganizationBillingsRequest : SortingParams, IMapWith<GetBillingByOrganizationId.Query>
{
    public Guid OrganizationId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetOrganizationBillingsRequest, GetBillingByOrganizationId.Query>()
            .ForMember(c => c.OrganizationId, opt => opt.MapFrom(r => r.OrganizationId))
            .ForMember(c => c.PageNumber, opt => opt.MapFrom(r => r.PageNumber))
            .ForMember(c => c.PageSize, opt => opt.MapFrom(r => r.PageSize))
            .ForMember(c => c.ColumnName, opt => opt.MapFrom(r => r.ColumnName))
            .ForMember(c => c.Descending, opt => opt.MapFrom(r => r.Descending));
    }
}
