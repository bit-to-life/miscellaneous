using Microsoft.Extensions.Options;
using System.Net;

namespace BitToLife.Miscellaneous.ExternalApi.Naver;

public sealed class NaverShortUrlService
{
    public NaverShortUrlService(HttpClient http, IOptions<NaverOptions> options)
    {
        NaverOptions _options = options.Value;

        _http = http;
        _http.BaseAddress = new Uri("https://openapi.naver.com/");
        _http.DefaultRequestHeaders.Add("X-Naver-Client-Id", _options.ClientId);
        _http.DefaultRequestHeaders.Add("X-Naver-Client-Secret", _options.ClientSecret);
    }

    private readonly HttpClient _http;

    private async Task<string?> PostAsync(string url)
    {
        using HttpResponseMessage response = await _http.GetAsync($"/v1/util/shorturl?url={WebUtility.UrlEncode(url)}");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        return null;
    }

    public async Task<string?> GetShortUrlAsync(string url)
    {
        string? result = await PostAsync(url);

        if (!string.IsNullOrWhiteSpace(result))
        {
            dynamic json = System.Text.Json.JsonSerializer.Deserialize<object>(result)!;

            return json.result.url;
        }

        return null;
    }
}
