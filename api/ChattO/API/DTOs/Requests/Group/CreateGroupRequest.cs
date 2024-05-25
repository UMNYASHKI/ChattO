using Application.Groups.Commands;
using Application.Helpers.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Group;

public class CreateGroupRequest : IMapWith<Create.Command>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public Guid OrganizationId { get; set; }
    public List<Guid> UsersId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateGroupRequest, Create.Command>()
            .ForMember(org => org.Name, opt => opt.MapFrom(c => c.Name))
            .ForMember(org => org.OrganizationId, opt => opt.MapFrom(c => c.OrganizationId))
            .ForMember(org => org.UsersId, opt => opt.MapFrom(c => c.UsersId));
    }
}