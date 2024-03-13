using Application.Helpers.Mappings;
using Application.Organizations.Commands;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Organization;

public class CreateOrganizationRequest : IMapWith<CreateOrganization.Command>
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Domain { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

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

        //create map with userCommand
    }
}
