namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

public sealed record KakaoOptions
{
    public string RestAPIKey { get; init; } = string.Empty;

    public string AdminKey { get; init; } = string.Empty;
}
