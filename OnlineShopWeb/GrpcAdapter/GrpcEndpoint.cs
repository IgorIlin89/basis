using Microsoft.Extensions.Configuration;

namespace GrpcAdapter;

public record GrpcEndpoint : NamedResolverConfiguration
{
    public string ApiKey { get; init; }

    public int? MaxReceiveMessageSize { get; init; }

    public static GrpcEndpoint Bind(IConfiguration configuration, string sectionName)
    {
        GrpcEndpoint grpcEndpoint = new GrpcEndpoint();
        configuration.GetSection(sectionName).Bind(grpcEndpoint);
        return grpcEndpoint;
    }
}
