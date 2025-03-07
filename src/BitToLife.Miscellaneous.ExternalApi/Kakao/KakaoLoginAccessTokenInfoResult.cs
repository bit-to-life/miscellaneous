namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

#pragma warning disable IDE1006 // Naming Styles
public sealed record KakaoLoginAccessTokenInfoResult
{
    public long id { get; init; }

    public int expires_in { get; init; }

    public int app_id { get; init; }
}
#pragma warning restore IDE1006 // Naming Styles
