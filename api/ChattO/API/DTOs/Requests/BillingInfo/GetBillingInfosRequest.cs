using API.DTOs.Paging;
using API.DTOs.Requests.Billing;
using API.DTOs.Sorting;
using Application.BillInfo.Queries;
using Application.Helpers.Mappings;
using Application.Payment.Queries;
using AutoMapper;
using Domain.Enums;

namespace API.DTOs.Requests.BillingInfo;

public class GetBillingInfosRequest : SortingParams, IMapWith<Get.Query>
{
    public BillingType? Type { get; set; }

    public double? Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetBillingInfosRequest, Get.Query>()
            .ForMember(c => c.Type, opt => opt.MapFrom(r => r.Type))
            .ForMember(c => c.Price, opt => opt.MapFrom(r => r.Price))
            .ForMember(c => c.PageNumber, opt => opt.MapFrom(r => r.PageNumber))
            .ForMember(c => c.PageSize, opt => opt.MapFrom(r => r.PageSize))
            .ForMember(c => c.ColumnName, opt => opt.MapFrom(r => r.ColumnName))
            .ForMember(c => c.Descending, opt => opt.MapFrom(r => r.Descending));
    }
}
