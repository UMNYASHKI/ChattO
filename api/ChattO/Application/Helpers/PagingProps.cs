namespace Application.Helpers;

public class PagingProps
{
    public string? ColumnName { get; set; } = "Id";
    public bool? Descending { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
