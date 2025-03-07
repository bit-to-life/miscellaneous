namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

#pragma warning disable IDE1006 // Naming Styles
public sealed record KakaoGeocodingResult
{
    public Meta meta { get; init; } = default!;

    public IEnumerable<Document> documents { get; init; } = [];

    public sealed record Meta
    {
        public int total_count { get; init; }

        public int pageable_count { get; init; }

        public bool is_end { get; init; }
    }

    public sealed record Document
    {
        public string address_name { get; init; } = string.Empty;

        public string address_type { get; init; } = string.Empty;

        public string x { get; init; } = string.Empty;

        public string y { get; init; } = string.Empty;

        public JibunAddress address { get; init; } = default!;

        public RoadAddress road_address { get; init; } = default!;

        public sealed record JibunAddress
        {
            public string address_name { get; init; } = string.Empty;

            public string region_1depth_name { get; init; } = string.Empty;

            public string region_2depth_name { get; init; } = string.Empty;

            public string region_3depth_name { get; init; } = string.Empty;

            public string region_3depth_h_name { get; init; } = string.Empty;

            public string h_code { get; init; } = string.Empty;

            public string b_code { get; init; } = string.Empty;

            public string mountain_yn { get; init; } = string.Empty;

            public string main_address_no { get; init; } = string.Empty;

            public string sub_address_no { get; init; } = string.Empty;

            public string x { get; init; } = string.Empty;

            public string y { get; init; } = string.Empty;
        }

        public sealed record RoadAddress
        {
            public string address_name { get; init; } = string.Empty;

            public string region_1depth_name { get; init; } = string.Empty;

            public string region_2depth_name { get; init; } = string.Empty;

            public string region_3depth_name { get; init; } = string.Empty;

            public string road_name { get; init; } = string.Empty;

            public string underground_yn { get; init; } = string.Empty;

            public string main_building_no { get; init; } = string.Empty;

            public string sub_building_no { get; init; } = string.Empty;

            public string building_name { get; init; } = string.Empty;

            public string zone_no { get; init; } = string.Empty;

            public string x { get; init; } = string.Empty;

            public string y { get; init; } = string.Empty;
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
