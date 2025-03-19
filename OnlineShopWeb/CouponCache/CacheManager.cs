namespace CouponCache;

internal class CacheManager
{
    private readonly Func<CancellationToken, Task> _refreshFunction;
    private readonly object _lock = new();
    private Task? _initTask;
    private bool _isInitialized;

    internal CacheManager(Func<CancellationToken, Task> refreshFunction)
    {
        _refreshFunction = refreshFunction;
    }

    internal ValueTask WaitUntilCacheIsReadyAsync()
    {
        lock (_lock)
        {
            if (_isInitialized)
            {
                return ValueTask.CompletedTask;
            }

            // Beim Initialisieren wird kein CancellationToken verwendet,
            // damit der Cache unabhängig von Aufrufern zu Ende aufgebaut werden kann.
            return _initTask != null
                ? new ValueTask(_initTask)
                : new ValueTask(RefreshInternalAsync(true, CancellationToken.None));
        }
    }

    internal Task RefreshAsync(CancellationToken cancellationToken)
    {
        lock (_lock)
        {
            return _initTask ?? RefreshInternalAsync(!_isInitialized, cancellationToken);
        }
    }

    private async Task RefreshInternalAsync(bool isInitializing, CancellationToken cancellationToken)
    {
        try
        {
            if (isInitializing)
            {
                _initTask = _refreshFunction(cancellationToken);
                await _initTask;
            }
            else
            {
                await _refreshFunction(cancellationToken);
            }

            _isInitialized = true;
        }
        finally
        {
            _initTask = null;
        }
    }
}