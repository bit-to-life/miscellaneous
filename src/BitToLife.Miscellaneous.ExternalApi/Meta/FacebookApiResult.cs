namespace BitToLife.Miscellaneous.ExternalApi.Meta;

public sealed class FacebookApiResult<T> where T : class
{
    public bool IsSuccess { get; init; }

    public FacebookErrorInfo? Error { get; init; }

    public T? Value { get; init; }
}