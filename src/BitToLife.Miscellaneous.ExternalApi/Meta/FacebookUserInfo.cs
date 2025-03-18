using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.Meta;

#pragma warning disable IDE1006 // Naming Styles
/// <summary>
/// Facebook user information.
/// <br />
/// https://developers.facebook.com/docs/graph-api/reference/user/
/// </summary>
public sealed record FacebookUserInfo
{
    public required string id { get; init; }

    public string? first_name { get; init; }

    public string? last_name { get; init; }

    public string? name { get; init; }

    public string? name_format { get; init; }

    public string? short_name { get; init; }

    public Picture? picture { get; init; }

    public string? email { get; init; }

    public string? birthday { get; init; }

    public string? gender { get; init; }

    public AgeRange? age_range { get; init; }

    public sealed record AgeRange
    {
        public int? min { get; init; }
        public int? max { get; init; }
    }

    public sealed record Picture
    {
        public Data? data { get; init; }

        public sealed record Data
        {
            public int? width { get; init; }

            public int? height { get; init; }

            public bool? is_silhouette { get; init; }

            public string? url { get; init; }
        }
    }

    public static FacebookUserInfo GetUserInfo(ClaimsPrincipal claimsPrincipal)
    {
        FacebookUserInfo userInfo = new()
        {
            id = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
            first_name = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value,
            last_name = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Surname).Value,
            name = claimsPrincipal.Claims.Single(c => c.Type == "name").Value,
            email = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
            picture = new Picture
            {
                data = new Picture.Data
                {
                    url = claimsPrincipal.Claims.Single(c => c.Type == "picture").Value
                }
            }
        };

        return userInfo;
    }
}
#pragma warning restore IDE1006 // Naming Styles