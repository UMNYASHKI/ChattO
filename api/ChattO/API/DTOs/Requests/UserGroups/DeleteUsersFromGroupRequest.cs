using Application.Helpers.Mappings;
using Application.UserGroups.Commands;
using AutoMapper;

namespace API.DTOs.Requests.UserGroups;

public class DeleteUsersFromGroupRequest : IMapWith<Delete.Command>
{
    public Guid GroupId { get; set; }

    public ICollection<Guid> UsersId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddUsersToGroupRequest, Delete.Command>()
            .ForMember(org => org.GroupId, opt => opt.MapFrom(c => c.GroupId))
            .ForMember(org => org.UsersId, opt => opt.MapFrom(c => c.UsersId));
    }
}
