namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

#pragma warning disable IDE1006 // Naming Styles
public sealed record KakaoSearchKeywordResult
{
    public Meta meta { get; init; } = default!;

    public IEnumerable<Document> documents { get; init; } = [];

    public sealed record Meta
    {
        public SameName same_name { get; init; } = default!;

        public int pageable_count { get; init; }

        public int total_count { get; init; }

        public bool is_end { get; init; }

        public sealed record SameName
        {
            public IEnumerable<string> region { get; init; } = [];

            public string keyword { get; init; } = string.Empty;

            public string selected_region { get; init; } = string.Empty;
        }
    }

    public sealed record Document
    {
        public string id { get; init; } = string.Empty;

        public string place_name { get; init; } = string.Empty;

        public string category_name { get; init; } = string.Empty;

        public string category_group_code { get; init; } = string.Empty;

        public string category_group_name { get; init; } = string.Empty;

        public string phone { get; init; } = string.Empty;

        public string address_name { get; init; } = string.Empty;

        public string road_address_name { get; init; } = string.Empty;

        public string x { get; init; } = string.Empty;

        public string y { get; init; } = string.Empty;

        public string place_url { get; init; } = string.Empty;

        public string distance { get; init; } = string.Empty;
    }
}
#pragma warning restore IDE1006 // Naming Styles
