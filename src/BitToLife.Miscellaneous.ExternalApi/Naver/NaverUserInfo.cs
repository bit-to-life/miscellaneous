namespace BitToLife.Miscellaneous.ExternalApi.Naver;

#pragma warning disable IDE1006 // Naming Styles
public sealed record NaverUserInfo
{
    public string resultcode { get; set; } = string.Empty;

    public string? message { get; set; }

    public Response? response { get; set; }

    public sealed record Response
    {
        public string? email { get; set; }

        public string? nickname { get; set; }

        public string? profile_image { get; set; }

        public string? age { get; set; }

        public string? gender { get; set; }

        public string? id { get; set; }

        public string? name { get; set; }

        public string? birthday { get; set; }

        public string? birthyear { get; set; }

        public string? mobile { get; set; }
    }
}
#pragma warning restore IDE1006 // Naming Styles
