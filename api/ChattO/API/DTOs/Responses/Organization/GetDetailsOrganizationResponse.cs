using Application.Helpers.Mappings;
using AutoMapper;

namespace API.DTOs.Responses.Organization;

public class GetDetailsOrganizationResponse : IMapWith<Domain.Models.Organization>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Domain { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Organization, GetDetailsOrganizationResponse>()
            .ForMember(q => q.Id, opt => opt.MapFrom(r => r.Id))
            .ForMember(q => q.Name, opt => opt.MapFrom(r => r.Name))
            .ForMember(q => q.Domain, opt => opt.MapFrom(r => r.Domain));
    }
}
