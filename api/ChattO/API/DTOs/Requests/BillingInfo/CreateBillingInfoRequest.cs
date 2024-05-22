using API.DTOs.Requests.Organization;
using Application.BillInfo.Commands;
using Application.Helpers.Mappings;
using Application.Organizations.Commands;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.BillingInfo;

public class CreateBillingInfoRequest : IMapWith<CreateBillingInfo.Command>
{
    [Required]
    public BillingType Type { get; set; }

    [Required]
    public double Price { get; set; }

    public Currency Currency { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBillingInfoRequest, CreateBillingInfo.Command>()
            .ForMember(c => c.Type, opt => opt.MapFrom(r => r.Type))
            .ForMember(c => c.Currency, opt => opt.MapFrom(r => r.Currency))
            .ForMember(c => c.Price, opt => opt.MapFrom(r => r.Price));

    }
}
