using Domain.Models.Files;

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

}
