using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed record AppleTokenValidationResult(
    bool IsValid,
    ClaimsPrincipal? ClaimsPrincipal,
    string? ErrorMessage
);