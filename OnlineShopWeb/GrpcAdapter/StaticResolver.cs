using Grpc.Net.Client.Balancer;

namespace GrpcAdapter;

internal sealed class StaticResolver : Resolver
{
    private readonly List<BalancerAddress> _addresses;

    /// <summary>
    /// Initializes a new instance of the <see cref="Grpc.Net.Client.Balancer.StaticResolver"/> class with the specified addresses.
    /// </summary>
    /// <param name="addresses">The resolved addresses.</param>
    public StaticResolver(IEnumerable<BalancerAddress> addresses)
    {
        _addresses = addresses.ToList();
    }

    public override void Start(Action<ResolverResult> listener)
    {
        // Send addresses to listener once. They will never change.
        listener(ResolverResult.ForResult(_addresses, serviceConfig: null, serviceConfigStatus: null));
    }
}