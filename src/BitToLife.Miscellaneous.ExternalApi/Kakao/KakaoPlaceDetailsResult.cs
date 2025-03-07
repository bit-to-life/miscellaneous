namespace BitToLife.Miscellaneous.ExternalApi.Kakao;

#pragma warning disable IDE1006 // Naming Styles
public sealed record KakaoPlaceDetailsResult
{
    public bool isExist { get; init; } = default!;

    public BasicInfo basicInfo { get; init; } = default!;

    public MenuInfo? menuInfo { get; init; }

    public PhotoInfo? photo { get; init; }

    public sealed record BasicInfo
    {
        public long cid { get; init; }

        public string placenamefull { get; init; } = string.Empty;

        public string? englishname { get; init; }

        public string? phonenum { get; init; }

        public string? homepage { get; init; }

        public IEnumerable<CommunityList>? communityList { get; init; }

        public Category category { get; init; } = default!;

        public Address address { get; init; } = default!;

        public OperationInfo? operationInfo { get; init; }

        public FacilityInfo? facilityInfo { get; init; }

        public IEnumerable<string>? tags { get; init; }

        public OpenHour? openHour { get; init; }

        public sealed record CommunityList
        {
            public string community { get; init; } = string.Empty;

            public string communitynoprotocol { get; init; } = string.Empty;
        }

        public sealed record Category
        {
            public string cate1name { get; init; } = string.Empty;

            public string cateid { get; init; } = string.Empty;

            public string catename { get; init; } = string.Empty;

            public string fullCateIds { get; init; } = string.Empty;
        }

        public sealed record Address
        {
            public string addrbunho { get; init; } = string.Empty;

            public string? addrdetail { get; init; }

            public NewAddress newaddr { get; init; } = default!;

            public Region region { get; init; } = default!;

            public sealed record NewAddress
            {
                public string bsizonno { get; init; } = string.Empty;

                public string newaddrfull { get; init; } = string.Empty;
            }

            public sealed record Region
            {
                public string fullname { get; init; } = string.Empty;

                public string name3 { get; init; } = string.Empty;

                public string newaddrfullname { get; init; } = string.Empty;
            }

        }

        public sealed record OperationInfo
        {
            public string? appointment { get; init; }

            public string? delivery { get; init; }

            public string? pagekage { get; init; }
        }

        public sealed record FacilityInfo
        {
            public string? fordisabled { get; init; }

            public string? nursery { get; init; }

            public string? parking { get; init; }

            public string? pet { get; init; }

            public string? wifi { get; init; }

            public string? smokingroom { get; init; }
        }

        public sealed record OpenHour
        {
            public IEnumerable<Offday>? offdayList { get; init; }

            public IEnumerable<Period>? periodList { get; init; }

            public sealed record Offday
            {
                public string holidayName { get; init; } = string.Empty;

                public string? temporaryHolidays { get; init; }

                public string? weekAndDay { get; init; }
            }

            public sealed record Period
            {
                public string periodName { get; init; } = string.Empty;

                public IEnumerable<Time> timeList { get; init; } = [];

                public sealed record Time
                {
                    public string dayOfWeek { get; init; } = string.Empty;

                    public string timeName { get; init; } = string.Empty;

                    public string timeSE { get; init; } = string.Empty;
                }
            }
        }
    }

    public sealed record MenuInfo
    {
        public int menuboardphotocount { get; init; }

        public int menucount { get; init; }

        public string? productyn { get; init; }

        public string timeexp { get; init; } = string.Empty;

        public IEnumerable<Menu> menuList { get; init; } = [];

        public sealed record Menu
        {
            public string menu { get; init; } = string.Empty;

            public string? price { get; init; }

            public bool recommend { get; init; }

            public string? img { get; init; }
        }
    }

    public sealed record PhotoInfo
    {
        public IEnumerable<PhotoCategory> photoList { get; init; } = [];

        public sealed record PhotoCategory
        {
            public string categoryName { get; init; } = string.Empty;

            public IEnumerable<Photo> list { get; init; } = [];

            public sealed record Photo
            {
                public string photoid { get; init; } = string.Empty;

                public string orgurl { get; init; } = string.Empty;
            }
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
