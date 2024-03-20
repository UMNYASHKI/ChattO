namespace API.DTOs.Sorting;

public class SortingParams
{
    public string? ColumnName { get; set; } = "Id";

    public bool? Descending { get; set; } = true;
}
