
namespace CouponCache
{
    public interface ICacheManager
    {
        Task Refresh(CancellationToken cancellationToken);
    }
}