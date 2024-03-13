using Application.Helpers.Mappings;
using Application.Organizations.Commands;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Orqanization;

public class UpdateOrganizationRequest : IMapWith<UpdateOrganization.Command>
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Domain { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateOrganizationRequest, UpdateOrganization.Command>()
            .ForMember(c => c.Name, opt => opt.MapFrom(r => r.Name))
            .ForMember(c => c.Domain, opt => opt.MapFrom(r => r.Domain));
    }
}
