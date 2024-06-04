using Application.Feeds.Commands;
using Application.Helpers.Mappings;
using AutoMapper;

namespace API.DTOs.Requests.Feed;

public class UpdateFeedRequest : IMapWith<UpdateFeed.Command>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    //public IFormFile? FeedImage { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateFeedRequest, UpdateFeed.Command>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
