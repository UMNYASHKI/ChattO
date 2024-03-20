using Application.Helpers.Mappings;
using Domain.Models.Files;

namespace API.DTOs.Responses.File;

public class FileResponse : IMapWith<BaseFile>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }
}
