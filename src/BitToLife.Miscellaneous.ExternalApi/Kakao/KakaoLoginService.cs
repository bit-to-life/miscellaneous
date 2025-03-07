using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

public sealed class KakaoLoginService
{
    public KakaoLoginService(HttpClient apiHttp, IOptions<KakaoOptions> options)
    {
        _kakaoOptions = options.Value;

        _apiHttp = apiHttp;
        _apiHttp.BaseAddress = new Uri("https://kapi.kakao.com");
    }

    private readonly KakaoOptions _kakaoOptions;
    private readonly HttpClient _apiHttp;

    private const string GET_USER_INFO = "/v2/user/me";
    private const string ACCESS_TOKEN_INFO = "/v1/user/access_token_info";

    public async Task<string> GetUserInfoAsync(int userId)
    {
        var parameters = new Dictionary<string, string?>
        {
            ["target_id_type"] = "user_id",
            ["target_id"] = $"{userId}"
        };

        var url = QueryHelpers.AddQueryString(GET_USER_INFO, parameters);
        _apiHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("KakaoAK", _kakaoOptions.AdminKey);
        var response = await _apiHttp.GetStringAsync(url);

        return response;
    }

    public async Task<KakaoUserInfoResult> GetUserInfoAsync(string accessToken)
    {
        _apiHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        var response = await _apiHttp.GetAsync(GET_USER_INFO);

        var body = await response.Content.ReadAsStringAsync();

        var result = new KakaoUserInfoResult(
            response.IsSuccessStatusCode,
            response.IsSuccessStatusCode ? JsonSerializer.Deserialize<KakaoUserInfo>(body) : default
        );

        return result;
    }

    public async Task<KakaoLoginAccessTokenInfoResult> GetAccessTokenInfoAsync(string accessToken)
    {
        _apiHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        var response = await _apiHttp.GetStringAsync(ACCESS_TOKEN_INFO);
        var result = JsonSerializer.Deserialize<KakaoLoginAccessTokenInfoResult>(response)!;

        return result;
    }
}
