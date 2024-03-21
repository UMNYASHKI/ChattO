using API.DTOs.Paging;

namespace API.DTOs.Sorting;

public class SortingParams : PagingParams
{
    public string? ColumnName { get; set; } = "Id";

    public bool? Descending { get; set; } = true;
}
