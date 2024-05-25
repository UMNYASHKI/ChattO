using Application.Helpers.Mappings;
using Application.Organizations.Commands;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Organization;

public class CreateOrganizationRequest : IMapWith<CreateOrganization.Command>, IMapWith<AppUser>
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Domain { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrganizationRequest, CreateOrganization.Command>()
            .ForMember(c => c.Name, opt => opt.MapFrom(r => r.Name))
            .ForMember(c => c.Domain, opt => opt.MapFrom(r => r.Domain));

        profile.CreateMap<CreateOrganizationRequest, AppUser>()
            .ForMember(u => u.Email, opt => opt.MapFrom(r => r.Email))
            .ForMember(u => u.UserName, opt => opt.MapFrom(r => r.UserName))
            .ForMember(u => u.PasswordHash, opt => opt.MapFrom(r => r.Password))
            .AfterMap((s, d) => d.Role = AppUserRole.SuperAdmin);
    }
}
