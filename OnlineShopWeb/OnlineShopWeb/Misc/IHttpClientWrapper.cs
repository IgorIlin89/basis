
namespace OnlineShopWeb.Misc
{
    public interface IHttpClientWrapper
    {
        void Delete(string basePath, params string[] args);
        Task<T> Get<T>(string basePath, params string[] args);
        Task<TOut> Post<TIn, TOut>(string basePath, TIn postObject, params string[] args);
        Task<T> Put<T>(string basePath, params object[] args);
    }
}