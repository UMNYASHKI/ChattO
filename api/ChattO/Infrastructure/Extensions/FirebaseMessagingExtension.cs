using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class FirebaseMessagingExtension
{
    public static void AddFirbaseApp(this IApplicationBuilder builder)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chatto-419213-firebase-adminsdk-xq7rm-ca2456e87b.json");
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Firebase Admin SDK file not found");
        }

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.GetApplicationDefault(),
        });
    }
}
