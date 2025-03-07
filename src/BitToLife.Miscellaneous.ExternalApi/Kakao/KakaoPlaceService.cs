using System.Text.Json;

namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

public sealed class KakaoPlaceService
{
    public KakaoPlaceService(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://place.map.kakao.com");
    }

    private readonly HttpClient _http;

    private const string PLACE_DETAIL_PATH = "/main/v/";

    /// <summary>
    /// 장소 상세정보 가져오기
    /// </summary>
    /// <param name="placeId">카카오맵 Place ID</param>
    /// <returns>Json string</returns>
    public async Task<(KakaoPlaceDetailsResult Result, string Json)> GetPlaceDetailsAsync(string placeId)
    {
        string response = await _http.GetStringAsync($"{PLACE_DETAIL_PATH}{placeId}");

        KakaoPlaceDetailsResult result = JsonSerializer.Deserialize<KakaoPlaceDetailsResult>(response)!;

        return (result, response);
    }
}
