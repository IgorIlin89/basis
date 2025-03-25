namespace Utility.Misc;

public interface IHttpClientWrapper
{
    void Delete((string ApiUrl, string ApiKey) connectionData, string basePath, params string[] args);
    Task<T> Get<T>((string ApiUrl, string ApiKey) connectionData, string basePath, params string[] args);
    Task<TOut> Post<TIn, TOut>((string ApiUrl, string ApiKey) connectionData, string basePath, TIn postObject, params string[] args);
    Task<TOut> Put<TIn, TOut>((string ApiUrl, string ApiKey) connectionData, string basePath, TIn postObject, params string[] args);

    Task Get(string apiUrl, string basePath,
       string[] args);
}