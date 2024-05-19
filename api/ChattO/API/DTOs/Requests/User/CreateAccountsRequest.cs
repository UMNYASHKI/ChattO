using Application.AppUsers.Commands;
using Application.Helpers.Mappings;
using Domain.Enums;
using AutoMapper;

namespace API.DTOs.Requests.User;

public class CreateAccountsRequest : IMapWith<CreateAppUser.Command>
{
    public List<CreateAccountRequest> Requests { get; set; }

    public AppUserRole AppUserRole { get; set; }

    public Guid OrganizationId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateAccountsRequest, CreateAppUser.Command>()
            .ForMember(command => command.AppUsers,
                           opt => opt.MapFrom(c => c.Requests))
            .ForMember(command => command.Role,
                           opt => opt.MapFrom(c => c.AppUserRole))
            .ForMember(command => command.OrganizationId,
                           opt => opt.MapFrom(c => c.OrganizationId));
    }
}
