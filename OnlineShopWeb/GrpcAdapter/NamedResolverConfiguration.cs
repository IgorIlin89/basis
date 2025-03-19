namespace GrpcAdapter;

public record NamedResolverConfiguration
{
    public string Name { get; init; }

    public int Port { get; init; }

    public IEnumerable<string> Hosts { get; init; }
}
