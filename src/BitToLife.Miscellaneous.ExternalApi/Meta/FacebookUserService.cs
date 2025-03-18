using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Meta;

public sealed class FacebookUserService : ApiClientBase
{
    public FacebookUserService(HttpClient http) : base(http)
    {
        http.BaseAddress = new Uri(BASE_ADDRESS);
    }

    private const string BASE_ADDRESS = "https://graph.facebook.com";

    public static readonly string[] DEFAULT_PUBLIC_PROFILE_FIELDS =
    [
        "id",
        "first_name",
        "last_name",
        "name",
        "name_format",
        "short_name",
        "picture"
    ];

    public async Task<FacebookApiResult<FacebookUserInfo>> GetUserInfoAsync(string accessToken, string[]? fields = null, CancellationToken cancellationToken = default)
    {
        IDictionary<string, string?> parameters = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken,
            ["fields"] = string.Join(",", fields ?? [])
        };

        string url = QueryHelpers.AddQueryString("/v22.0/me", parameters);
        HttpResponseMessage response = await Http.GetAsync(url, cancellationToken);
        bool isSuccessed = response.IsSuccessStatusCode;
        string body = await response.Content.ReadAsStringAsync(cancellationToken);

        FacebookApiResult<FacebookUserInfo> result = new()
        {
            IsSuccess = isSuccessed,
            Value = isSuccessed ? JsonSerializer.Deserialize<FacebookUserInfo>(body) : null,
            Error = !isSuccessed ? JsonSerializer.Deserialize<FacebookErrorInfo>(body) : null
        };

        return result;
    }
}
