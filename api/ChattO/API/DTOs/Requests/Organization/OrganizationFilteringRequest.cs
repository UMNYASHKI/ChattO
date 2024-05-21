using API.DTOs.Sorting;
using Application.Helpers.Mappings;
using Application.Organizations.Queries;
using AutoMapper;

namespace API.DTOs.Requests.Organization;

public class OrganizationFilteringRequest : SortingParams, IMapWith<GetListOrganizations.Query>
{
    public string? Name { get; set; }
    public string? Domain { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrganizationFilteringRequest, GetListOrganizations.Query>()
            .ForMember(q => q.Name, opt => opt.MapFrom(r => r.Name))
            .ForMember(q => q.Domain, opt => opt.MapFrom(r => r.Domain))
            .ForMember(q => q.ColumnName, opt => opt.MapFrom(r => r.ColumnName))
            .ForMember(q => q.Descending, opt => opt.MapFrom(r => r.Descending))
            .ForMember(q => q.PageNumber, opt => opt.MapFrom(r => r.PageNumber))
            .ForMember(q => q.PageSize, opt => opt.MapFrom(r => r.PageSize));
    }
}
