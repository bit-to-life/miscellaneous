using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

public sealed class KakaoLocalService
{
    public KakaoLocalService(HttpClient http, IOptions<KakaoOptions> options)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://dapi.kakao.com");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("KakaoAK", options.Value.RestAPIKey);
    }

    private readonly HttpClient _http;

    public static class Categories
    {
        /// <summary>
        /// 음식점
        /// </summary>
        public static string Restaurant => "FD6";

        /// <summary>
        /// 카페
        /// </summary>
        public static string Cafe => "CE7";
    }

    private const string GEOCODING_PATH = "/v2/local/search/address.JSON";
    private const string SEARCH_KEYWORD_PATH = "/v2/local/search/keyword.JSON";

    /// <summary>
    /// 주소로 좌표 가져오기
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public async Task<KakaoGeocodingResult> GeocodingAsync(string address)
    {
        Dictionary<string, string?> parameters = new()
        {
            { "query", address }
        };

        string url = QueryHelpers.AddQueryString(GEOCODING_PATH, parameters);
        string response = await _http.GetStringAsync(url);

        KakaoGeocodingResult result = JsonSerializer.Deserialize<KakaoGeocodingResult>(response)!;

        return result;
    }

    private async Task<KakaoSearchKeywordResult> SearchKeywordAsync(string keyword, string? category = null, int? page = null)
    {
        Dictionary<string, string?> parameters = new()
        {
            { "query", keyword },
            { "category_group_code", category ?? string.Empty },
            { "page",  $"{page ?? 1}" }
        };

        string url = QueryHelpers.AddQueryString(SEARCH_KEYWORD_PATH, parameters);
        string response = await _http.GetStringAsync(url);
        KakaoSearchKeywordResult result = JsonSerializer.Deserialize<KakaoSearchKeywordResult>(response)!;

        return result;
    }

    /// <summary>
    /// 키워드로 장소 검색
    /// </summary>
    /// <param name="keyword">검색할 키워드</param>
    /// <param name="category">카테고리</param>
    /// <returns></returns>
    public async Task<IEnumerable<KakaoSearchKeywordResult>> SearchKeywordAsync(string keyword, string? category = null, int pageCount = 5)
    {
        List<KakaoSearchKeywordResult> list = [];
        int page = 1;
        while (true)
        {
            KakaoSearchKeywordResult result = await SearchKeywordAsync(keyword, category, page: page);
            list.Add(result);

            if (list.Count == pageCount)
            {
                break;
            }

            if (!result.meta.is_end)
            {
                page++;
            }
            else
            {
                break;
            }
        }

        return [.. list];
    }
}
