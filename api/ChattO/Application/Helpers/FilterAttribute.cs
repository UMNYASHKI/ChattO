namespace Application.Helpers;

[AttributeUsage(AttributeTargets.Property)]
public class FilterAttribute : Attribute
{
    public bool IsFilterable { get; set; } = true;
}
