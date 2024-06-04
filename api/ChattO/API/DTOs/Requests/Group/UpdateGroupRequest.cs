using Application.Groups.Commands;
using Application.Helpers.Mappings;
using AutoMapper;

namespace API.DTOs.Requests.Group;

public class UpdateGroupRequest : IMapWith<Update.Command>
{
    public string? Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateGroupRequest, Update.Command>()
            .ForMember(org => org.Name, opt => opt.MapFrom(c => c.Name));
    }
}
