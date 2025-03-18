using BitToLife.Miscellaneous.ExternalApi.OIDC;

namespace BitToLife.Miscellaneous.ExternalApi.Meta;

public sealed class FacebookAuthKeyService(HttpClient http) : AuthKeyServiceBase(http, BASE_ADDRESS, PATH)
{
    public const string BASE_ADDRESS = "https://www.facebook.com";

    private const string PATH = "/.well-known/oauth/openid/jwks";
}
