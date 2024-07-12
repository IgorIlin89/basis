
using System.Net;

namespace OnlineShopWeb.Misc
{
    public interface IHttpClientWrapper
    {
        Task<T> Get<T>(string methodName);
        Task<T> Get<T>(string methodName, int id);
        Task<T> Post<T>(string methodName, T body);
        void Delete<T>(string methodName, T param);
        Task<HttpStatusCode> Put<T>(string methodName, T body);
    }
}