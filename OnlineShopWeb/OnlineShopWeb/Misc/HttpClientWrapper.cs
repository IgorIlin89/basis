using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Misc;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new HttpClient();
    UriBuilder _uriBuilder;
    private readonly string ApiUrl;
    private readonly string ApiKey;

    public HttpClientWrapper(IOptions<HttpClientWrapperOptions> options)
    {
        ApiUrl = options.Value.ApiUrl;
        //_uriBuilder = new UriBuilder(options.Value.ApiUrl);
        _uriBuilder = new UriBuilder();
        _uriBuilder.Scheme = "https";
        _uriBuilder.Host = options.Value.ApiHost;
        _uriBuilder.Port = Int32.Parse(options.Value.ApiPort);

    }

    public async Task<T> Get<T>(string methodName)
    {
        _uriBuilder.Path = methodName;
        var response = await _httpClient.GetAsync(_uriBuilder.Uri);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(content);

    }

    public async Task<T> Get<T>(params string[] args)
    {
        if (args.Length == 1)
        {
            _uriBuilder.Path = args[0] + "/list";
        }
        else if (args.Length == 2)
        {
            _uriBuilder.Path = args[0];
            _uriBuilder.Query = "id=" + args[1];

            //var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            //query["id"] = args[1];
        }

        var response = await _httpClient.GetAsync(_uriBuilder.Uri);
        var content = await response.Content.ReadFromJsonAsync<T>();

        return content;

    }

    public async Task<HttpStatusCode> Delete(string methodName, string id)
    {
        var response = await _httpClient.DeleteAsync(ApiUrl + "/" + methodName + id);
        return response.StatusCode;
    }

    public async Task<T> Post<T>(string methodName, T body)
    {
        var httpBody = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    Application.Json);

        var response = await _httpClient.PostAsync(ApiUrl + "/" + methodName, httpBody);
        var content = await response.Content.ReadFromJsonAsync<T>();

        if (content == null)
        {
            throw new NullReferenceException();
        }

        return content;
    }

    public async Task<T> Put<T>(string methodName, T body)
    {
        var httpBody = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    Application.Json);

        var response = await _httpClient.PutAsync(ApiUrl + "/" + methodName, httpBody);
        var content = await response.Content.ReadFromJsonAsync<T>();

        if (content == null)
        {
            throw new NullReferenceException();
        }

        return content;
    }
}
