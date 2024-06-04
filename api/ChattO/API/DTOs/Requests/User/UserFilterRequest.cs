using API.DTOs.Sorting;
using Application.AppUsers.Queries;
using Application.Helpers.Mappings;
using Domain.Enums;
using AutoMapper;

namespace API.DTOs.Requests.User;

public class UserFilterRequest : SortingParams, IMapWith<GetListAppUsers.Query>
{
    public Guid? GroupId { get; set; }

    public string? UserName { get; set; }

    public AppUserRole? AppUserRole { get; set; }

    public string? Email { get; set; }

    public Guid? OrganizationId { get; set; }

    public bool? IsEmailSent { get; set; }

    public string? DisplayName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserFilterRequest, GetListAppUsers.Query>()
            .ForMember(query => query.GroupId,
                                  opt => opt.MapFrom(request => request.GroupId))
            .ForMember(query => query.UserName,
                                  opt => opt.MapFrom(request => request.UserName))
            .ForMember(query => query.Role,
                                  opt => opt.MapFrom(request => request.AppUserRole))
            .ForMember(query => query.Email,
                                  opt => opt.MapFrom(request => request.Email))
            .ForMember(query => query.OrganizationId,
                                  opt => opt.MapFrom(request => request.OrganizationId))
            .ForMember(query => query.IsEmailSent,
                                  opt => opt.MapFrom(request => request.IsEmailSent))
            .ForMember(query => query.DisplayName,
                                  opt => opt.MapFrom(request => request.DisplayName))
            .ForMember(c => c.PageNumber, opt => opt.MapFrom(r => r.PageNumber))
            .ForMember(c => c.PageSize, opt => opt.MapFrom(r => r.PageSize))
            .ForMember(c => c.ColumnName, opt => opt.MapFrom(r => r.ColumnName))
            .ForMember(c => c.Descending, opt => opt.MapFrom(r => r.Descending));
    }
}
