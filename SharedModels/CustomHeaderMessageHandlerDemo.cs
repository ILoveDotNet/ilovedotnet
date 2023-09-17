namespace SharedModels;

public class CustomHeaderMessageHandlerDemo : DelegatingHandler
{
    public CustomHeaderMessageHandlerDemo(HttpClientHandler httpClientHandler) : base(httpClientHandler)
    {
        InnerHandler = httpClientHandler;
    }    

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-Client-ID", "https://ilovedotnet.org");
        return await base.SendAsync(request, cancellationToken);
    }
}