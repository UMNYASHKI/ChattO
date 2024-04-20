using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class FirebaseMessagingExtension
{
    public static void AddFirbaseApp(this IApplicationBuilder builder)
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chatto-419213-firebase-adminsdk-xq7rm-ca2456e87b.json")),
        });
    }
}
