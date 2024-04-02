using Application.Helpers;
using Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface ICloudRepository
{
    Task<Result<bool>> UploadFile(IFormFile file, string path);

    Task<Result<IFormFile>> DownloadFile(string fileName);
}
