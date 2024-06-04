using API.DTOs.Responses.Billing;
using Application.Helpers.Mappings;
using AutoMapper;

namespace API.DTOs.Responses.Group;

public class GroupResponse : IMapWith<Domain.Models.Group>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<AppUserGroupResponse> Users { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Group, GroupResponse>()
           .ForMember(c => c.Id, opt => opt.MapFrom(r => r.Id))
           .ForMember(c => c.Name, opt => opt.MapFrom(r => r.Name))
           .ForMember(c => c.Users, opt => opt.MapFrom(r => r.AppUserGroups));
    }
}
