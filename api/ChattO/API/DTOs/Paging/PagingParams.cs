using API.DTOs.Requests.Organization;
using Application.Helpers.Mappings;
using Application.Organizations.Commands;
using Application.Payment.Queries;
using AutoMapper;

namespace API.DTOs.Paging;

public class PagingParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
