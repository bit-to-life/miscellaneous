namespace BitToLife.Miscellaneous.ExternalApi.Google;

public abstract class GoogleApiBase : ApiClientBase
{
    protected GoogleApiBase(HttpClient http) : base(http)
    {
        Http.BaseAddress = new Uri(BASE_ADDRESS);
    }

    private const string BASE_ADDRESS = "https://www.googleapis.com";
}
