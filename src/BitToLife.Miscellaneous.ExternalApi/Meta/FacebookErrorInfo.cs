namespace BitToLife.Miscellaneous.ExternalApi.Meta;

#pragma warning disable IDE1006 // Naming Styles
public sealed record FacebookErrorInfo
{
    public required Error error { get; init; }

    public sealed record Error
    {
        public required string message { get; set; }

        public required string type { get; set; }

        public required int code { get; set; }

        public int? error_subcode { get; set; }

        public string? error_user_title { get; set; }

        public string? error_user_msg { get; set; }

        public required string fbtrace_id { get; set; }
    }
}
#pragma warning restore IDE1006 // Naming Styles