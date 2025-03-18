using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.OIDC;
public sealed record TokenValidationResult
{
    public bool IsValid { get; init; }

    public ClaimsPrincipal? ClaimsPrincipal { get; init; }

    public string? ErrorMessage { get; init; }
}