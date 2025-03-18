using BitToLife.Miscellaneous.ExternalApi.OIDC;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed class AppleAuthKeyService(HttpClient http) : AuthKeyServiceBase(http, BASE_ADDRESS, PATH)
{
    public const string BASE_ADDRESS = "https://appleid.apple.com";

    private const string PATH = "/auth/keys";
}
