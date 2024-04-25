using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class FirebaseMessagingExtension
{
    public static void AddFirbaseApp(this IApplicationBuilder builder)
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "chatto-419213-firebase-adminsdk-xq7rm-ca2456e87b.json"));

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.GetApplicationDefault(),
        });
    }
}
