using API.DTOs.Sorting;
using Application.Feeds.Queries;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;

namespace API.DTOs.Requests.Feed;

public class FeedFilterRequest : SortingParams, IMapWith<GetListFeeds.Query>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public FeedType? Type { get; set; }
    public Guid? GroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FeedFilterRequest, GetListFeeds.Query>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
            .ForMember(c => c.PageNumber, opt => opt.MapFrom(r => r.PageNumber))
            .ForMember(c => c.PageSize, opt => opt.MapFrom(r => r.PageSize))
            .ForMember(c => c.ColumnName, opt => opt.MapFrom(r => r.ColumnName))
            .ForMember(c => c.Descending, opt => opt.MapFrom(r => r.Descending));
    }
}
