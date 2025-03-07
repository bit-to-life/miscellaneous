namespace BitToLife.Miscellaneous.ExternalApi.Google.OAuth;

public sealed record GoogleApiResult<T> where T : class
{
    public bool IsSuccess { get; init; }

    public GoogleErrorInfo? Error { get; init; }

    public T? Value { get; init; }
}