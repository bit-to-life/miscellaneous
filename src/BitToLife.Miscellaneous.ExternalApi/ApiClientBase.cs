namespace BitToLife.Miscellaneous.ExternalApi;

public abstract class ApiClientBase(HttpClient http)
{
    public HttpClient Http { get; private init; } = http;
}
