using API.DTOs.Responses.File;
using Application.Helpers.Mappings;
using Domain.Enums;
using Domain.Models;
using AutoMapper;

namespace API.DTOs.Responses.User;

public class UserResponse : IMapWith<AppUser>
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string DisplayName { get; set; }

    public AppUserRole Role { get; set; }

    public bool IsEmailSent { get; set; }

    public FileResponse ProfileImage { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, UserResponse>()
            .ForMember(q => q.Id, opt => opt.MapFrom(r => r.Id))
            .ForMember(q => q.UserName, opt => opt.MapFrom(r => r.UserName))
            .ForMember(q => q.Email, opt => opt.MapFrom(r => r.Email))
            .ForMember(q => q.DisplayName, opt => opt.MapFrom(r => r.DisplayName))
            .ForMember(q => q.Role, opt => opt.MapFrom(r => r.Role))
            .ForMember(q => q.IsEmailSent, opt => opt.MapFrom(r => r.IsEmailSent))
            .ForMember(q => q.ProfileImage, opt => opt.MapFrom(r => r.ProfileImage));
    }
}
