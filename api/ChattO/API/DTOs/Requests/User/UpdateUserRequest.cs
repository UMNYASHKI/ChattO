using API.DTOs.Sorting;
using Application.AppUsers.Commands;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;

namespace API.DTOs.Requests.User;

public class UpdateUserRequest : IMapWith<UpdateAppUser.Command>
{
    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    //public IFormFile? ProfileImage { get; set; } 

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserRequest, UpdateAppUser.Command>()
            .ForMember(command => command.DisplayName,
                                   opt => opt.MapFrom(c => c.DisplayName))
            .ForMember(command => command.Email,
                                   opt => opt.MapFrom(c => c.Email))
            .ForMember(command => command.UserName,
                                   opt => opt.MapFrom(c => c.UserName));
    }
}
