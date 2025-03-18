using Microsoft.IdentityModel.Tokens;

namespace BitToLife.Miscellaneous.ExternalApi.OIDC;

public abstract class AuthKeyServiceBase : ApiClientBase
{
    protected AuthKeyServiceBase(HttpClient http, string baseAddress, string path) : base(http)
    {
        Http.BaseAddress = new Uri(baseAddress);
        _path = path;
    }

    private readonly string _path;

    public async Task<JsonWebKeySet> GetKeySetAsync(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await Http.GetAsync(_path, cancellationToken);
        string json = await response.Content.ReadAsStringAsync(cancellationToken);

        return new JsonWebKeySet(json);
    }
}
