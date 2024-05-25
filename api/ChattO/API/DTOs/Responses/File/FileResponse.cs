using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models.Files;

namespace API.DTOs.Responses.File;

public class FileResponse : IMapWith<BaseFile>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BaseFile, FileResponse>()
            .ForMember(q => q.Id, opt => opt.MapFrom(r => r.Id))
            .ForMember(q => q.Name, opt => opt.MapFrom(r => r.Name))
            .ForMember(q => q.Url, opt => opt.MapFrom(r => r.PublicUrl));
    }
}
