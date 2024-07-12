using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Misc;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string ApiUrl;
    private readonly string ApiKey;

    public HttpClientWrapper(IOptions<HttpClientWrapperOptions> options)
    {
        ApiUrl = options.Value.ApiUrl;
    }

    public async Task<T> Get<T>(string methodName)
    {
        var request = await _httpClient.GetAsync(ApiUrl + "/" + methodName);
        var response = await request.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(response);

    }

    public async Task<T> Get<T>(string methodName, int id)
    {
        var request = await _httpClient.GetAsync(ApiUrl + "/" + methodName+id);
        var response = await request.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(response);

    }

    public async void Delete<T>(string methodName, T param)
    {
        var request = await _httpClient.GetAsync(ApiUrl + "/" + methodName + param);
    }

    public async Task<T> Post<T>(string methodName, T body)
    {
        var httpBody = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    Application.Json);

        var request = await _httpClient.PostAsync(ApiUrl + "/" + methodName, httpBody);
        var response = await request.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(response);
    }

    public async Task<HttpStatusCode> Put<T>(string methodName, T body)
    {
        var httpBody = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    Application.Json);

        var request = await _httpClient.PostAsync(ApiUrl + "/" + methodName, httpBody);

        return request.StatusCode;
    }
}
