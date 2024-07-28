using System.Net;

namespace OnlineShopWeb.Misc
{
    public interface IHttpClientWrapper
    {
        void Delete(string methodName, params string[] args);
        Task<T> Get<T>(string basePath, params string[] args);
        Task<T> Post<T>(string basePath, params Object[] args);
        Task<T> Put<T>(string basePath, params Object[] args);
    }
}