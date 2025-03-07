using Microsoft.Extensions.FileProviders;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed record AppleOptions
{
    public string BundleId { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string KeyId { get; set; } = string.Empty;

    public string TeamId { get; set; } = string.Empty;

    public IFileInfo KeyFileInfo { get; set; } = default!;
}
