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
    private readonly string ApiHost;
    private readonly string ApiPort;
    private readonly Uri _baseUri;


    public HttpClientWrapper(IOptions<HttpClientWrapperOptions> options)
    {
        ApiUrl = options.Value.ApiUrl;
        ApiHost = options.Value.ApiHost;
        ApiPort = options.Value.ApiPort;
        _uriBuilder = new UriBuilder();
        _baseUri = new Uri(options.Value.ApiScheme);
        //_uriBuilder = new UriBuilder(options.Value.ApiUrl);
        //_uriBuilder.Scheme = "https";
        //_uriBuilder.Host = options.Value.ApiHost;
        //_uriBuilder.Port = Int32.Parse(options.Value.ApiPort);

    }

    public async Task<T> Get<T>(string methodName)
    {
        _uriBuilder.Path = methodName;
        var response = await _httpClient.GetAsync(_uriBuilder.Uri);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(content);

    }

    public async Task<T> Get<T>(string basePath, params string[] args)
    {
        var relativeUri = "/" + basePath + "/";
        var uriBuilder = new UriBuilder();

        for (var i = 0; i < args.Length; i++)
        {
            relativeUri += args[i] + "/";
        }

        var uri = new Uri(_baseUri, relativeUri);
        var uri2 = new Uri("https://localhost:7149/product/1");
        var z = uri.Query;
        var xy = uri.UserInfo;
        var xzt = uri.Segments;
        var gdfg = uri.ToString();
        var y = uri.AbsoluteUri;
        var x = uri.IsWellFormedOriginalString();


        //_uriBuilder.Path = basePath + pathToAdd;

        var response = await _httpClient.GetAsync(uri);


        // beliebig viele varianten muessen unterstuetz werrden,

        //wir schmeißen keine exceptions selbst
        //ueberflussige "/" in den args werden entfernt
        //Get<List<ProductDto>>("product", "////list"); muss abgefangen werden

        // TODO CREATE A CUSTOM EXCEPTION

        if (response.StatusCode != HttpStatusCode.OK)
        {
            // return // TODO Read content, write to log, throw custom exception
        }

        var content = await response.Content.ReadFromJsonAsync<T>();

        return content;

        //input: _httpClientWrapper.Get<ProductDto>("product") output: https://localhost:500/product
        //input: _httpClientWrapper.Get<ProductDto>("product", "list") output: https://localhost:500/product/list
        //input: _httpClientWrapper.Get<ProductDto>("product", 1) output: https://localhost:500/product/1
        //input: _httpClientWrapper.Get<ProductDto>("product",1,2,3) output: https://localhost:500/product/1/2/3
        //input: _httpClientWrapper.Get<ProductDto>("product","email","igor@gmail") output: https://localhost:500/product/email/igor@gmail.com
        //optional //input: _httpClientWrapper.Get<ProductDto>("product",1,2,3) output: https://localhost:500/product?id=1&&id2=2&&id3=3

        //_uriBuilder.Query = args[0];
        //var uri = c;
        //var uriString = uri.ToString();
        //var requestString = uriString.Replace("?", "/");///falschh
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
