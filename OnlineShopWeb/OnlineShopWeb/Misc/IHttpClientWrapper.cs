
namespace OnlineShopWeb.Misc
{
    public interface IHttpClientWrapper
    {
        void Delete(string apiUrl, string basePath, params string[] args);
        Task<T> Get<T>(string apiUrl, string basePath, params string[] args);
        Task<TOut> Post<TIn, TOut>(string apiUrl, string basePath, TIn postObject, params string[] args);
        Task<TOut> Put<TIn, TOut>(string apiUrl, string basePath, TIn postObject, params string[] args);
    }
}