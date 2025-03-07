using System.Net.Http.Headers;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Naver;

public sealed class NaverLoginService
{
    public NaverLoginService(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://openapi.naver.com");
    }

    private readonly HttpClient _http;

    private const string GET_USER_INFO = "/v1/nid/me";

    public async Task<NaverUserInfoResult> GetUserInfoAsync(string accessToken)
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        string response = await _http.GetStringAsync(GET_USER_INFO);

        NaverUserInfo userInfo = JsonSerializer.Deserialize<NaverUserInfo>(response)!;
        NaverUserInfoResult result = new()
        {
            IsSuccess = userInfo.resultcode == "00",
            UserInfo = userInfo
        };

        return result;
    }
}
