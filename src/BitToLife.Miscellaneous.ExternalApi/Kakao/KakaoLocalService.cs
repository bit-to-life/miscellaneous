using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

public sealed class KakaoLocalService
{
    public KakaoLocalService(HttpClient apiHttp, HttpClient mapHttp, IOptions<KakaoOptions> options)
    {
        _apiHttp = apiHttp;
        _apiHttp.BaseAddress = new Uri("https://dapi.kakao.com");
        _apiHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("KakaoAK", options.Value.RestAPIKey);

        _mapHttp = mapHttp;
        _mapHttp.BaseAddress = new Uri("https://place.map.kakao.com");
    }

    private readonly HttpClient _apiHttp;
    private readonly HttpClient _mapHttp;

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
    private const string PLACE_DETAIL_PATH = "/main/v/";

    /// <summary>
    /// 주소로 좌표 가져오기
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public async Task<KakaoGeocodingResult> GeocodingAsync(string address)
    {
        var parameters = new Dictionary<string, string?>
        {
            ["query"] = address
        };

        var url = QueryHelpers.AddQueryString(GEOCODING_PATH, parameters);
        var response = await _apiHttp.GetStringAsync(url);

        var result = JsonSerializer.Deserialize<KakaoGeocodingResult>(response)!;

        return result;
    }

    private async Task<KakaoSearchKeywordResult> SearchKeywordAsync(string keyword, string? category = null, int? page = null)
    {
        var parameters = new Dictionary<string, string?>
        {
            ["query"] = keyword,
            ["category_group_code"] = category ?? string.Empty,
            ["page"] = $"{page ?? 1}"
        };

        var url = QueryHelpers.AddQueryString(SEARCH_KEYWORD_PATH, parameters);
        var response = await _apiHttp.GetStringAsync(url);
        var result = JsonSerializer.Deserialize<KakaoSearchKeywordResult>(response)!;

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
        var list = new List<KakaoSearchKeywordResult>();
        var page = 1;
        while (true)
        {
            var result = await SearchKeywordAsync(keyword, category, page: page);
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

    /// <summary>
    /// 장소 상세정보 가져오기
    /// </summary>
    /// <param name="placeId">카카오맵 Place ID</param>
    /// <returns>Json string</returns>
    public async Task<(KakaoPlaceDetailsResult Result, string Json)> GetPlaceDetailsAsync(string placeId)
    {
        var response = await _mapHttp.GetStringAsync($"{PLACE_DETAIL_PATH}{placeId}");

        var result = JsonSerializer.Deserialize<KakaoPlaceDetailsResult>(response)!;

        return (result, response);
    }
}
