using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace BitToLife.Miscellaneous.ExternalApi.Naver;

public sealed class NaverCloudPlatformService
{
    public NaverCloudPlatformService(HttpClient http, IOptions<NaverCloudPlatformOptions> options)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://naveropenapi.apigw.ntruss.com");
        _http.DefaultRequestHeaders.Add("X-NCP-APIGW-API-KEY-ID", options.Value.ClientId);
        _http.DefaultRequestHeaders.Add("X-NCP-APIGW-API-KEY", options.Value.ClientSecret);
    }

    private readonly HttpClient _http;

    private const string GEOCODING_PATH = "/map-geocode/v2/geocode";
    public async Task<string> GeocodingAsync(string address)
    {
        Dictionary<string, string?> parameters = new()
        {
            { "query", address },
            { "filter", "BCODE" }
        };

        string url = QueryHelpers.AddQueryString(GEOCODING_PATH, parameters);
        string response = await _http.GetStringAsync(url);

        return response;
    }


    private const string REVERSE_GEOCODING_PATH = "/map-reversegeocode/v2/gc";
    public async Task<string> ReverseGeocodingAsync(double[] coordinate)
    {
        Dictionary<string, string?> parameters = new()
        {
            { "coords", $"{coordinate[0]},{coordinate[1]}" },
            { "output", "json" },
            { "orders" , "roadaddr" }
        };

        string url = QueryHelpers.AddQueryString(REVERSE_GEOCODING_PATH, parameters);
        string response = await _http.GetStringAsync(url);

        return response;
    }
}
