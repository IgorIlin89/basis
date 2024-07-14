using System.Net;

namespace OnlineShopWeb.Misc
{
    public interface IHttpClientWrapper
    {
        Task<HttpStatusCode> Delete(string methodName, string id);
        //Task<T> Get<T>(string methodName);
        Task<T> Get<T>(params string[] args);
        Task<T> Post<T>(string methodName, T body);
        Task<T> Put<T>(string methodName, T body);
    }
}