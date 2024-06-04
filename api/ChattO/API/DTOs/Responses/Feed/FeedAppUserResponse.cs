using Application.Helpers.Mappings;
using AutoMapper;

namespace API.DTOs.Responses.Feed;

public class FeedAppUserResponse : IMapWith<Domain.Models.AppUserFeed>
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public bool IsCreator { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.AppUserFeed, FeedAppUserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AppUserId))
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
            .ForMember(dest => dest.IsCreator, opt => opt.MapFrom(src => src.IsCreator));
    }
}
