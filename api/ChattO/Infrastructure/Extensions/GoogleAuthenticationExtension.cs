using Application.Helpers;
using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Extensions;

public static class GoogleAuthenticationExtension
{
    public static Result<GoogleSigInModel> GetSigInData(this AuthenticateResult authResult)
    {
        var claims = authResult.Principal.Identities.FirstOrDefault()?.Claims;
        var email = claims.FirstOrDefault(c => c.Type.Contains("emailaddress")).Value;
        var provider = authResult.Principal.Identity.AuthenticationType;
        var providerKey = claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(providerKey))
        {
            return Result.Failure<GoogleSigInModel>("Failed to get sign in data");
        }

        return Result.Success(new GoogleSigInModel
        {
            Email = email,
            Provider = provider,
            ProviderKey = providerKey
        });
    }
    public class GoogleSigInModel
    {
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }
}
