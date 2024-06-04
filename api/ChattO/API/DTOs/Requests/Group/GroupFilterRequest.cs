using API.DTOs.Sorting;
using Application.Groups.Commands;
using Application.Groups.Queries;
using Application.Helpers.Mappings;
using AutoMapper;

namespace API.DTOs.Requests.Group;

public class GroupFilterRequest : SortingParams, IMapWith<Get.Query>
{
    public string? Name { get; set; }
    public Guid? OrganizationId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GroupFilterRequest, Get.Query>()
            .ForMember(org => org.Name, opt => opt.MapFrom(c => c.Name))
            .ForMember(org => org.OrganizationId, opt => opt.MapFrom(c => c.OrganizationId))
            .ForMember(c => c.PageNumber, opt => opt.MapFrom(r => r.PageNumber))
            .ForMember(c => c.PageSize, opt => opt.MapFrom(r => r.PageSize))
            .ForMember(c => c.ColumnName, opt => opt.MapFrom(r => r.ColumnName))
            .ForMember(c => c.Descending, opt => opt.MapFrom(r => r.Descending));
    }
}
