using Domain.Enums;
using Domain.Models.Files;
using Domain.Models;
using API.DTOs.Responses.Organization;
using Application.Helpers.Mappings;
using API.DTOs.Responses.File;
using AutoMapper;

namespace API.DTOs.Responses.User;

public class UserDetailsResponse : IMapWith<AppUser>
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string DisplayName { get; set; }

    public AppUserRole Role { get; set; }

    public bool IsEmailSent { get; set; }

    public GetDetailsOrganizationResponse Organization { get; set; } 

    public FileResponse ProfileImage { get; set; } 

    //public ICollection<GroupResponse> AppUserGroups { get; set; }

    //public ICollection<FeedResponse> AppUserFeeds { get; set; }

    //public ICollection<TicketResponse> Tickets { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, UserDetailsResponse>()
            .ForMember(q => q.Id, opt => opt.MapFrom(r => r.Id))
            .ForMember(q => q.UserName, opt => opt.MapFrom(r => r.UserName))
            .ForMember(q => q.Email, opt => opt.MapFrom(r => r.Email))
            .ForMember(q => q.DisplayName, opt => opt.MapFrom(r => r.DisplayName))
            .ForMember(q => q.Role, opt => opt.MapFrom(r => r.Role))
            .ForMember(q => q.IsEmailSent, opt => opt.MapFrom(r => r.IsEmailSent));
    }
}
