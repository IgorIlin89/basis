
namespace OnlineShopWeb.Misc
{
    public interface IHttpClientWrapper
    {
        Task<T> Get<T>(string methodName);
        Task<T> Get<T>(string methodName, int id);
        Task<T> Post<T>(string methodName, T body);
        void Delete<T>(string methodName, T param);
    }
}