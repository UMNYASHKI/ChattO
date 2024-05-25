using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;

namespace API.DTOs.Responses.Group;

public class UserInGroupResponse : IMapWith<AppUser>
{
    public string Email { get; set; }

    public string DisplayName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, UserInGroupResponse>()
           .ForMember(c => c.Email, opt => opt.MapFrom(r => r.Email))
           .ForMember(c => c.DisplayName, opt => opt.MapFrom(r => r.DisplayName));
    }
}
