namespace BitToLife.Miscellaneous.ExternalApi.Naver;

public sealed record NaverUserInfoResult
{
    public bool IsSuccess { get; init; }

    public NaverUserInfo UserInfo { get; init; } = default!;
}
