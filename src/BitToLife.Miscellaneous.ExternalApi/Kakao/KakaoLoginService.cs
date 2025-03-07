using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

public sealed class KakaoLoginService
{
    public KakaoLoginService(HttpClient http, IOptions<KakaoOptions> options)
    {
        _kakaoOptions = options.Value;

        _http = http;
        _http.BaseAddress = new Uri("https://kapi.kakao.com");
    }

    private readonly KakaoOptions _kakaoOptions;
    private readonly HttpClient _http;

    private const string GET_USER_INFO = "/v2/user/me";
    private const string ACCESS_TOKEN_INFO = "/v1/user/access_token_info";

    public async Task<string> GetUserInfoAsync(int userId)
    {
        Dictionary<string, string?> parameters = new()
        {
            { "target_id_type", "user_id" },
            { "target_id", $"{userId}" }
        };

        string url = QueryHelpers.AddQueryString(GET_USER_INFO, parameters);
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("KakaoAK", _kakaoOptions.AdminKey);
        string response = await _http.GetStringAsync(url);

        return response;
    }

    public async Task<KakaoUserInfoResult> GetUserInfoAsync(string accessToken)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        HttpResponseMessage response = await _http.GetAsync(GET_USER_INFO);

        string body = await response.Content.ReadAsStringAsync();

        KakaoUserInfoResult result = new(
            response.IsSuccessStatusCode,
            response.IsSuccessStatusCode ? JsonSerializer.Deserialize<KakaoUserInfo>(body) : default
        );

        return result;
    }

    public async Task<KakaoLoginAccessTokenInfoResult> GetAccessTokenInfoAsync(string accessToken)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        string response = await _http.GetStringAsync(ACCESS_TOKEN_INFO);
        KakaoLoginAccessTokenInfoResult result = JsonSerializer.Deserialize<KakaoLoginAccessTokenInfoResult>(response)!;

        return result;
    }
}
