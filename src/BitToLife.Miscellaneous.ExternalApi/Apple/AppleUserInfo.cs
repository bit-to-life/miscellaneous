using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed class AppleUserInfo
{
    public string Id { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }

    public static AppleUserInfo GetUserInfo(ClaimsPrincipal claimsPrincipal, string? givenName = null, string? familyName = null)
    {
        string? id = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        string? email = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        string? name = familyName is not null && givenName is not null ? $"{givenName} {familyName}".Trim() : email[..email.IndexOf('@')];

        AppleUserInfo userInfo = new()
        {
            Id = id,
            Email = email,
            Name = name,
            GivenName = givenName,
            FamilyName = familyName
        };

        return userInfo;
    }
}
