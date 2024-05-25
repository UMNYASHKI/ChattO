using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;

namespace API.DTOs.Responses.Feed;

public class FeedResponse : IMapWith<Domain.Models.Feed>
{
    public Guid FeedId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FeedType Type { get; set; }
    public Guid? GroupId { get; set; }
    public string? FeedImageUrl { get; set; } 
    //public List<FeedAppUserResponse> AppUsers { get; set; } //TODO: Create another enpoint for this

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Feed, FeedResponse>()
            .ForMember(dest => dest.FeedId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.FeedImageUrl, opt => opt.MapFrom(src => src.FeedImage.PublicUrl));
            //.ForMember(dest => dest.AppUsers, opt => opt.MapFrom(src => src.AppUserFeeds));
    }
}
