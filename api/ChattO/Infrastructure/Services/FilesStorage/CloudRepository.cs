using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Bytewizer.Backblaze.Client;
using Bytewizer.Backblaze.Models;
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

    public async Task<Result<bool>> DeleteFile(string fileName)
    {
        try
        {
            await _storageClient.ConnectAsync();
            var fileItem = (await _storageClient.Files.GetEnumerableAsync(new ListFileNamesRequest(_bucketSettings.Id))).FirstOrDefault(file => file.FileName == fileName);
            await _storageClient.Files.DeleteAsync(fileItem.FileId, fileName);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Failure<bool>(ex.Message);
        }
    }

    public async Task<bool> FileExists(string fileName)
    {
        await _storageClient.ConnectAsync();

        var fileItemExists = (await _storageClient.Files.GetEnumerableAsync(new ListFileNamesRequest(_bucketSettings.Id))).Any(file => file.FileName == fileName);

        if (!fileItemExists)
        {
            return false;
        }

        return true;
    }

    public async Task<Result<bool>> UploadFile(IFormFile file, string path)
    {
        var buffer = new byte[FilesConstants.UploadBufferSize];

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

    public string GetFileUrl(string fileName) 
    {
        return $"https://f005.backblazeb2.com/file/{_bucketSettings.Name}/{fileName}";
    }
}
