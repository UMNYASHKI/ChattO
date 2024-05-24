using Application.Feeds.Commands;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;

namespace API.DTOs.Requests.Feed;

public class CreateFeedRequest : IMapWith<CreateFeed.Command>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public FeedType Type { get; set; }
    public Guid? GroupId { get; set; }
    public List<Guid> AppUsersId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFeedRequest, CreateFeed.Command>()
            .ForMember(dest => dest.AppUsersId, opt => opt.MapFrom(src => src.AppUsersId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
    }
}
