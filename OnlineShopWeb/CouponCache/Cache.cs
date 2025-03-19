using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain;

namespace CouponCache;

internal class Cache : ICache
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CacheManager _cacheManager;
    private List<Coupon> _coupons = new List<Coupon>();

    public Cache(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _cacheManager = new CacheManager(RefreshCache);
    }

    public async ValueTask<List<Coupon>> GetCoupons()
    {
        await _cacheManager.WaitUntilCacheIsReadyAsync();

        return _coupons;
    }

    public async Task Refresh(CancellationToken cancellationToken)
    {
        await _cacheManager.RefreshAsync(cancellationToken);
    }

    private async Task RefreshCache(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IGetCouponListCommandHandler>();

        _coupons = await handler.Handle();
    }
}
