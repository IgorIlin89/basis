using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Misc;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new HttpClient();
    UriBuilder _uriBuilder;
    private readonly string ApiUrl;
    private readonly string ApiHost;
    private readonly string ApiPort;
    private readonly string _baseUri;


    public HttpClientWrapper(IOptions<HttpClientWrapperOptions> options)
    {
        ApiUrl = options.Value.ApiUrl;
        ApiHost = options.Value.ApiHost;
        ApiPort = options.Value.ApiPort;
        _uriBuilder = new UriBuilder();
        _baseUri = options.Value.ApiUrl;
    }

    public async Task<T> Get<T>(string basePath, params string[] args)
    {
        var list = new List<string>();

        list.Add(basePath);
        list.AddRange(args);

        var relativeUri = string.Join("/", list.Select(o => o.Replace("/", "")));
        var uri = new Uri(new Uri(_baseUri), relativeUri);

        var response = await _httpClient.GetAsync(uri);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return default;
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        //options maybe in program.cs? 

        var content = await response.Content.ReadFromJsonAsync<T>(options);

        return content;
    }

    public async void Delete(string basePath, params string[] args)
    {
        var list = new List<string>();


        foreach (var element in args)
        {
            list.Add(basePath);
            list.Add(element);

            var relativeUri = string.Join("/", list.Select(o => o.Replace("/", "")));

            var uri = new Uri(new Uri(_baseUri), relativeUri);

            var response = await _httpClient.DeleteAsync(uri);

            list.Clear();
        }
    }

    public async Task<T> Post<T>(string basePath, params Object[] args)
    {
        var list = new List<string>();
        StringContent httpBody = new StringContent("", Encoding.UTF8, Application.Json);
        T body = default;

        list.Add(basePath);

        foreach (var element in args)
        {
            if (element.GetType() == typeof(string))
            {
                list.Add(element.ToString());
            }
            else if (element.GetType() == typeof(T))
            {
                httpBody = new StringContent(
                    JsonSerializer.Serialize(element),
                    Encoding.UTF8,
                    Application.Json);

                body = (T)element;
            }
        }

        var relativeUri = string.Join("/", list.Select(o => o.Replace("/", "")));
        var uri = new Uri(new Uri(_baseUri), relativeUri);

        var response = await _httpClient.PostAsync(uri, httpBody);

        return body;
    }

    public async Task<T> Put<T>(string basePath, params Object[] args)
    {
        var list = new List<string>();
        StringContent httpBody = new StringContent("", Encoding.UTF8, Application.Json);
        T body = default;

        list.Add(basePath);

        foreach (var element in args)
        {
            if (element.GetType() == typeof(string))
            {
                list.Add(element.ToString());
            }
            else if (element.GetType() == typeof(T))
            {
                httpBody = new StringContent(
                    JsonSerializer.Serialize(element),
                    Encoding.UTF8,
                    Application.Json);

                body = (T)element;
            }
        }

        var relativeUri = string.Join("/", list.Select(o => o.Replace("/", "")));
        var uri = new Uri(new Uri(_baseUri), relativeUri);

        var response = await _httpClient.PutAsync(uri, httpBody);

        return body;
    }
}
