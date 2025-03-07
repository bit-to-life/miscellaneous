using Microsoft.IdentityModel.Tokens;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed class AppleAuthKeyService : ApiClientBase
{
    public AppleAuthKeyService(HttpClient http) : base(http)
    {
        Http.BaseAddress = new Uri(BASE_ADDRESS);
    }

    public const string BASE_ADDRESS = "https://appleid.apple.com";

    private const string PATH_AUTH_KEYS = "/auth/keys";

    public async Task<JsonWebKeySet> GetKeySetAsync(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await Http.GetAsync(PATH_AUTH_KEYS, cancellationToken);
        string json = await response.Content.ReadAsStringAsync(cancellationToken);

        return new JsonWebKeySet(json);
    }
}
