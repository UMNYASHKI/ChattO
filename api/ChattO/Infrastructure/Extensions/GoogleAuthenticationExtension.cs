using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Extensions;

public static class GoogleAuthenticationExtension
{
    public static GoogleSigInModel GetSigInData(this AuthenticateResult authResult)
    {
        var claims = authResult.Principal.Identities.FirstOrDefault()?.Claims;
        var email = claims.FirstOrDefault(c => c.Type.Contains("emailaddress")).Value;
        var provider = authResult.Principal.Identity.AuthenticationType;
        var providerKey = claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;

        return new GoogleSigInModel
        {
            Email = email,
            Provider = provider,
            ProviderKey = providerKey
        };
    }
    public class GoogleSigInModel
    {
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }
}
