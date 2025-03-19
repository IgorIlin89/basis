﻿using OnlineShopWeb.Domain.Exceptions;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Utility.Misc;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<T> Get<T>(string apiUrl, string basePath,
        string[] args)
    {
        //TODO
        ////CancellationToken cancellationToken, 
        Uri uri = CreateUri(apiUrl, basePath, args);

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        httpRequestMessage.Headers.Add("x-api-key", "1234567890!");

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            await CreateDomainExceptionFromErrorResponse(uri, response);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T> GetAsync<T>(string apiUrl, string basePath,
        CancellationToken cancellationToken,
        string[] args)
    {
        Uri uri = CreateUri(apiUrl, basePath, args);

        var response = await _httpClient.GetAsync(uri, cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            await CreateDomainExceptionFromErrorResponse(uri, response);
        }

        return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
    }

    public async void Delete(string apiUrl, string basePath, params string[] args)
    {
        var list = new List<string>();
        Uri uri = CreateUri(apiUrl, basePath, args);
        var response = await _httpClient.DeleteAsync(uri);
    }

    public async void DeleteAsync(string apiUrl, string basePath,
        CancellationToken cancellationToken, params string[] args)
    {
        var list = new List<string>();
        Uri uri = CreateUri(apiUrl, basePath, args);
        var response = await _httpClient.DeleteAsync(uri, cancellationToken);
    }

    public async Task<TOut> Post<TIn, TOut>(string apiUrl, string basePath, TIn postObject, params string[] args)
    {
        var uri = CreateUri(apiUrl, basePath, args);
        var response = await CreatePostOrPutRequest(postObject, uri, HttpMethod.Post);

        var result = await response.Content.ReadFromJsonAsync<TOut>();
        return result;
    }

    public async Task<TOut> PostAsync<TIn, TOut>(string apiUrl, string basePath,
        CancellationToken cancellationToken, TIn postObject, params string[] args)
    {
        var uri = CreateUri(apiUrl, basePath, args);
        var response = await CreatePostOrPutRequest(postObject, uri, HttpMethod.Post);

        var result = await response.Content.ReadFromJsonAsync<TOut>(cancellationToken);
        return result;
    }

    public async Task<TOut> Put<TIn, TOut>(string apiUrl, string basePath, TIn postObject, params string[] args)
    {
        var uri = CreateUri(apiUrl, basePath, args);
        var response = await CreatePostOrPutRequest(postObject, uri, HttpMethod.Put);

        var result = await response.Content.ReadFromJsonAsync<TOut>();
        return result;
    }

    public async Task<TOut> PutAsync<TIn, TOut>(string apiUrl, string basePath,
        CancellationToken cancellationToken, TIn postObject, params string[] args)
    {
        var uri = CreateUri(apiUrl, basePath, args);
        var response = await CreatePostOrPutRequest(postObject, uri, HttpMethod.Put);

        var result = await response.Content.ReadFromJsonAsync<TOut>();
        return result;
    }

    private Uri CreateUri(string apiUrl, string basePath, string[] args)
    {
        var list = new List<string>();

        list.Add(basePath);
        list.AddRange(args);

        var relativeUri = string.Join("/", list.Select(o => o.Replace("/", "")));
        var uri = new Uri(new Uri(apiUrl), relativeUri);
        return uri;
    }

    private async Task<HttpResponseMessage> CreatePostOrPutRequest<TIn>(TIn postObject, Uri uri, HttpMethod requestType)
    {
        var httpBody = new StringContent(
                            JsonSerializer.Serialize(postObject),
                            Encoding.UTF8,
                            Application.Json);

        var request = new HttpRequestMessage()
        {
            RequestUri = uri,
            Method = requestType,
            Content = httpBody
        };

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            var errorDto = JsonSerializer.Deserialize<ErrorDto>(errorResponse);

            throw new ApiException($"Exception in '{uri}'. Statuscode: '{response.StatusCode}'." +
                $" Response: '{errorDto.Message}' ExceptionType: '{errorDto.StatusCode}'");
        }
        else if (!response.IsSuccessStatusCode)
        {
            await CreateDomainExceptionFromErrorResponse(uri, response);
        }

        return response;
    }

    private async Task CreateDomainExceptionFromErrorResponse(Uri uri, HttpResponseMessage response)
    {
        //var errorResponse = await response.Content.ReadAsStringAsync();
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorDto = JsonSerializer.Deserialize<ErrorDto>(await response.Content.ReadAsStringAsync());
            throw new DomainException($"Exception in '{uri}'. Statuscode: '{response.StatusCode}'." +
            $" Response: '{errorDto.Message}' ExceptionType: '{errorDto.StatusCode}'");
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new DomainException($"Exception in '{uri}'. Statuscode: '{response.StatusCode}'." +
            $" Response: '{content}'");
        }

    }
}
