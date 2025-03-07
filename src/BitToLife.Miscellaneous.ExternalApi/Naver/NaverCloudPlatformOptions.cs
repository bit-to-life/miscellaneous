namespace BitToLife.Miscellaneous.ExternalApi.Naver;

public sealed record NaverCloudPlatformOptions
{
    public const string Key = nameof(NaverCloudPlatformOptions);

    public string ClientId { get; init; } = string.Empty;

    public string ClientSecret { get; init; } = string.Empty;
}
