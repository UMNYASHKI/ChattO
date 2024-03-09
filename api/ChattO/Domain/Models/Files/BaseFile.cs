namespace Domain.Models.Files;

public abstract class BaseFile
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
