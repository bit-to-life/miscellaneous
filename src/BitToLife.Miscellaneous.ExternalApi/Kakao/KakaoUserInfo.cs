namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

#pragma warning disable IDE1006 // Naming Styles
public sealed record KakaoUserInfo
{
    public long id { get; init; }

    public bool? has_signed_up { get; init; }

    public DateTimeOffset? connected_at { get; init; }

    public DateTimeOffset? synched_at { get; init; }

    public IDictionary<string, string>? properties { get; init; }

    public KakaoAccount? kakao_account { get; init; }

    public sealed record KakaoAccount
    {
        public bool? profile_needs_agreement { get; init; }

        public bool? profile_nickname_needs_agreement { get; init; }

        public bool? profile_image_needs_agreement { get; init; }

        public Profile? profile { get; init; }

        public bool? name_needs_agreement { get; init; }

        public string? name { get; init; }

        public bool? email_needs_agreement { get; init; }

        public bool? is_email_valid { get; init; }

        public bool? is_email_verified { get; init; }

        public string? email { get; init; }

        public bool? age_range_needs_agreement { get; init; }

        public string? age_range { get; init; }

        public bool? birthyear_needs_agreement { get; init; }

        public string? birthyear { get; init; }

        public bool? birthday_needs_agreement { get; init; }

        public string? birthday { get; init; }

        public string? birthday_type { get; init; }

        public bool? gender_needs_agreement { get; init; }

        public string? gender { get; init; }

        public bool? phone_number_needs_agreement { get; init; }

        public string? phone_number { get; init; }

        public bool? ci_needs_agreement { get; init; }

        public string? ci { get; init; }

        public DateTimeOffset? ci_authenticated_at { get; init; }

        public sealed record Profile
        {
            public string? nickname { get; init; }

            public string? thumbnail_image_url { get; init; }

            public string? profile_image_url { get; init; }

            public bool? is_default_image { get; init; }
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
