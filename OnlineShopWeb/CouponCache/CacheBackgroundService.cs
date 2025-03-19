using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CouponCache;

public class CacheBackgroundService : BackgroundService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceProvider _serviceProvider;

    public CacheBackgroundService(IHostApplicationLifetime applicationLifetime,
        IServiceProvider serviceProvider)
    {
        _applicationLifetime = applicationLifetime;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ApplicationStarted(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = _serviceProvider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<ICache>();

            await cache.Refresh(stoppingToken);

            await Task.Delay(30000, stoppingToken);
        }
    }

    private async Task ApplicationStarted(CancellationToken cancellationToken)
    {
        try
        {
            var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken,
                _applicationLifetime.ApplicationStarted);

            await Task.Delay(-1, cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            //we are waiting of this exception -> application is started
        }
    }
}
