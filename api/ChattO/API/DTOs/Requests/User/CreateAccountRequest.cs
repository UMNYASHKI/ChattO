using Application.AppUsers.Commands;
using Application.Helpers.Mappings;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.User;

public class CreateAccountRequest : IMapWith<CreateAppUser.Command.CommandUser>
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }  

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<CreateAccountRequest, CreateAppUser.Command.CommandUser>()
            .ForMember(user => user.FirstName,
                           opt => opt.MapFrom(c => c.FirstName))
            .ForMember(user => user.LastName,
                           opt => opt.MapFrom(c => c.LastName))
            .ForMember(user => user.UserName,
                           opt => opt.MapFrom(c => c.UserName))
            .ForMember(user => user.Email,
                           opt => opt.MapFrom(c => c.Email));
    }
}

