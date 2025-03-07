namespace BitToLife.Miscellaneous.ExternalApi.Google.OAuth;

#pragma warning disable IDE1006 // Naming Styles
public sealed record GoogleUserInfo
{
    public string sub { get; init; } = string.Empty;

    public string name { get; init; } = string.Empty;

    public string? given_name { get; init; }

    public string? family_name { get; init; }

    public string? picture { get; init; }

    public string email { get; init; } = string.Empty;

    public bool email_verified { get; init; }

    public string? locale { get; init; }

    public string hd { get; init; } = string.Empty;
}
#pragma warning restore IDE1006 // Naming Styles
