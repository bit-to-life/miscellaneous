namespace BitToLife.Miscellaneous.ExternalApi.Naver;

public sealed record NaverOptions
{
    public const string Key = nameof(NaverOptions);

    public string ClientId { get; init; } = string.Empty;

    public string ClientSecret { get; init; } = string.Empty;
}
