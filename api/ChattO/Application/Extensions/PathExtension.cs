using Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions;

public static class PathExtension
{
    public static string GetPath<T>(string domain, string fileName, string? feedId = null) where T : BaseFile
    {
        switch (typeof(T))
        {
            case Type type when type == typeof(ProfileImage):
                return $"{domain}/profiles/{fileName}";
            case Type type when type == typeof(FeedImage):
                if (feedId == null)
                {
                    return $"other/{fileName}";
                }

                return $"{domain}/feeds/{feedId}/{fileName}";
            case Type type when type == typeof(MessageFile):
                if (feedId == null)
                {
                    return $"other/{fileName}";
                }

                return $"{domain}/feeds/{feedId}/messages/{fileName}";
            default:
                return $"other/{fileName}";
        }
    }

    public static string GetFileExtension(this IFormFile file) 
    {
        return file.FileName.Split('.').Last();
    }

    public static string GetfileNameWithoutExtension(string fileName)
    {
        var separated = fileName.Split('.');
        return string.Join("", separated.Take(separated.Length - 1));
    }

    public static string GetImageUrl(string bucketName, string fileName) 
    {
        return $"https://f001.backblazeb2.com/file/{bucketName}/{fileName}";
    }
}
