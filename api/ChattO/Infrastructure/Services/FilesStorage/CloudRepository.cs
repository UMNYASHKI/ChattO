using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Bytewizer.Backblaze.Client;
using Domain.Models.Files;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.FilesStorage;

public class CloudRepository : ICloudRepository
{
    private readonly IStorageClient _storageClient;

    private readonly BucketSettings _bucketSettings;

    public CloudRepository(IStorageClient storageClient, IOptions<BucketSettings> bucketOptions)
    {
        _storageClient = storageClient;
        _bucketSettings = bucketOptions.Value;
    }

    public async Task<Result<byte[]>> DownloadFile(string fileName)
    {
        var buffer = new byte[1024 * 100];

        try
        {
            using (var memoryStream = new MemoryStream(buffer))
            {
                await _storageClient.ConnectAsync();

                var result = await _storageClient.DownloadAsync(_bucketSettings.Name, fileName, memoryStream);
                if (!result.IsSuccessStatusCode)
                {
                    return Result.Failure<byte[]>(result.Error.Message);
                }

            };

            return Result.Success(buffer);

        }
        catch (Exception ex)
        {
            return Result.Failure<byte[]>(ex.Message);
        }
    }

    public async Task<Result<bool>> UploadFile(IFormFile file, string path)
    {
        var buffer = new byte[1024 * 100];

        try
        {
            using var stream = file.OpenReadStream();

            await _storageClient.ConnectAsync();

            var result = await _storageClient.UploadAsync(_bucketSettings.Id, path, stream);
            if (!result.IsSuccessStatusCode)
            {
                return Result.Failure<bool>(result.Error.Message);
            }

            return Result.Success<bool>();
        }
        catch (Exception ex)
        {
            return Result.Failure<bool>(ex.Message);
        }
    }
}
