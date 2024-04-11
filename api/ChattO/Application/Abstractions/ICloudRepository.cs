using Application.Helpers;
using Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface ICloudRepository
{
    Task<Result<bool>> UploadFile(IFormFile file, string path);

    Task<bool> FileExists(string fileName);

    Task<Result<bool>> DeleteFile(string fileName);

    string GetFileUrl(string fileName);
}
