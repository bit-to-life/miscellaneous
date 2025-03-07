using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed class AppleUserInfo
{
    public string Id { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }

    public static AppleUserInfo GetUserInfo(ClaimsPrincipal claimsPrincipal, string? givenName, string? familyName)
    {
        var id = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var email = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        var name = familyName is not null && givenName is not null ? $"{familyName} {givenName}".Trim() : email[..email.IndexOf('@')];

        var userInfo = new AppleUserInfo
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
