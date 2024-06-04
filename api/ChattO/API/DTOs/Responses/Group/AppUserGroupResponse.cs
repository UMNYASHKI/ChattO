using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;

namespace API.DTOs.Responses.Group;

public class AppUserGroupResponse : IMapWith<AppUserGroup>
{
    public Guid Id { get; set; }
    public UserInGroupResponse UserInGroupResponse { get; set; }
    public bool IsModerator { get; set; }
    public Guid GroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUserGroup, AppUserGroupResponse>()
           .ForMember(c => c.Id, opt => opt.MapFrom(r => r.Id))
           .ForMember(c => c.IsModerator, opt => opt.MapFrom(r => r.IsModerator))
           .ForMember(c => c.GroupId, opt => opt.MapFrom(r => r.GroupId))
           .ForMember(c => c.UserInGroupResponse, opt => opt.MapFrom(r => r.AppUser));
    }
}
