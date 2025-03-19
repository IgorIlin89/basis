using Grpc.Net.Client.Balancer;

namespace GrpcAdapter;

public sealed class NamedResolverFactory : ResolverFactory
{
    private readonly string _resolverFactoryName;
    private readonly NamedResolverConfiguration _endpointsConfiguration;

    public NamedResolverFactory(string resolverFactoryName,
        NamedResolverConfiguration endpointsConfiguration)
    {
        _resolverFactoryName = resolverFactoryName;
        _endpointsConfiguration = endpointsConfiguration ?? throw new ArgumentNullException(nameof(endpointsConfiguration));
    }

    /// <inheritdoc />
    public override string Name => _resolverFactoryName;

    /// <inheritdoc />
    public override Resolver Create(ResolverOptions options)
    {
        var addresses = _endpointsConfiguration
            .Hosts.SelectMany(h => h.Split(';'))
            .Select(h => new BalancerAddress(h, _endpointsConfiguration.Port));

        return new StaticResolver(addresses);
    }
}