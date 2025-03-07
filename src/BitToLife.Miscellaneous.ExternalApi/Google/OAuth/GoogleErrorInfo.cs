namespace BitToLife.Miscellaneous.ExternalApi.Google.OAuth;

#pragma warning disable IDE1006 // Naming Styles
public sealed record GoogleErrorInfo
{
    public string error { get; init; } = string.Empty;

    public string error_description { get; init; } = string.Empty;
}
#pragma warning restore IDE1006 // Naming Styles