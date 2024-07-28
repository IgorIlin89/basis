using Microsoft.Extensions.Options;
using OnlineShopWeb.Domain.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Misc;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _baseUri;

    public HttpClientWrapper(IOptions<HttpClientWrapperOptions> options)
    {
        _baseUri = options.Value.ApiUrl;
    }

    public async Task<T> Get<T>(string basePath, params string[] args)
    {
        Uri uri = CreateUri(basePath, args);

        var response = await _httpClient.GetAsync(uri);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            await CreateDomainExceptionFromErrorResponse(uri, response);
        }

        return await response.Content.ReadFromJsonAsync<T>();
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

    public async Task<TOut> Post<TIn, TOut>(string basePath, TIn postObject, params string[] args)
    {
        var uri = CreateUri(basePath, args);

        var httpBody = new StringContent(
                    JsonSerializer.Serialize(postObject),
                    Encoding.UTF8,
                    Application.Json);

        var response = await _httpClient.PostAsync(uri, httpBody);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new ApiException(errorResponse);
        }
        else
        {
            await CreateDomainExceptionFromErrorResponse(uri, response);
        }

        var result = await response.Content.ReadFromJsonAsync<TOut>();
        return result;
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
    private Uri CreateUri(string basePath, string[] args)
    {
        var list = new List<string>();

        list.Add(basePath);
        list.AddRange(args);

        var relativeUri = string.Join("/", list.Select(o => o.Replace("/", "")));
        var uri = new Uri(new Uri(_baseUri), relativeUri);
        return uri;
    }

    private async Task CreateDomainExceptionFromErrorResponse(Uri uri, HttpResponseMessage response)
    {
        var errorResponse = await response.Content.ReadAsStringAsync();
        throw new DomainException($"Exception in '{uri}'. Statuscode: '{response.StatusCode}'. Response: '{errorResponse}'");
    }
}
