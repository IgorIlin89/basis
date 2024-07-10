using Microsoft.Extensions.Options;

namespace OnlineShopWeb.Misc;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly IOptions<HttpClientWrapperOptions> _options;

    public HttpClientWrapper(IOptions<HttpClientWrapperOptions> options)
    {
        _options = options;
    }

    public string ReturnTest()
    {
        return _options.Value.ApiUrl;
    }
}
