using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Google.OAuth;

public sealed class GoogleOAuth2Service(HttpClient http) : GoogleApiBase(http)
{
    private const string GET_USER_INFO = "/oauth2/v3/userinfo";

    public async Task<GoogleApiResult<GoogleUserInfo>> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        IDictionary<string, string?> parameters = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken
        };

        string url = QueryHelpers.AddQueryString(GET_USER_INFO, parameters);
        HttpResponseMessage response = await Http.GetAsync(url, cancellationToken);
        bool isSuccessed = response.IsSuccessStatusCode;
        string body = await response.Content.ReadAsStringAsync(cancellationToken);

        GoogleApiResult<GoogleUserInfo> result = new()
        {
            IsSuccess = isSuccessed,
            Value = isSuccessed ? JsonSerializer.Deserialize<GoogleUserInfo>(body) : null,
            Error = !isSuccessed ? JsonSerializer.Deserialize<GoogleErrorInfo>(body) : null
        };

        return result;
    }
}