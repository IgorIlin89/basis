

using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Balancer;
using Grpc.Net.Client.Configuration;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GrpcAdapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcAdapter(this IServiceCollection services, string sectionName, IConfiguration configuration)
    {
        var serviceConfiguration = GrpcEndpoint.Bind(configuration, sectionName);
        services
            .AddScoped<IInputAdapterGrpc, InputAdapterGrpc>()
            .AddGrpcClient<GrpcTestService.Contracts.TransactionService.TransactionServiceClient>(serviceConfiguration);
        return services;
    }


    private static void AddResolverFactory(this IServiceCollection services, string resolverFactoryName,
        GrpcEndpoint endpointConfiguration)
    {
        NamedResolverFactory implementationInstance = new NamedResolverFactory(resolverFactoryName, endpointConfiguration);
        services.AddSingleton((ResolverFactory)implementationInstance);
    }



    public static IHttpClientBuilder AddGrpcClient<T>(this IServiceCollection services, GrpcEndpoint endpointConfiguration) where T : class
    {
        string resolverFactoryName = typeof(T).FullName.Replace(".", "").Replace("+", "");
        services.AddResolverFactory(resolverFactoryName, endpointConfiguration);
        services.TryAddSingleton((NamedResolverConfiguration)endpointConfiguration);

        return services.AddGrpcClient<T>(delegate (GrpcClientFactoryOptions o)
        {
            o.Address = new Uri(resolverFactoryName + "://" + endpointConfiguration.Name);
        })
        //.AddApiKeyAuthentication(delegate (ApiKeyAuthenticationOptions options)
        //{
        //    options.ApiKey = endpointConfiguration.ApiKey;
        //})
        .ConfigureChannel(delegate (GrpcChannelOptions o)
        {
            //o.Credentials = (endpointConfiguration.UseSsl ? ChannelCredentials.SecureSsl : ChannelCredentials.Insecure);
            //o.UnsafeUseInsecureChannelCallCredentials = !endpointConfiguration.UseSsl;

            o.Credentials = ChannelCredentials.Insecure;
            o.UnsafeUseInsecureChannelCallCredentials = true;

            o.ServiceConfig = new ServiceConfig
            {
                LoadBalancingConfigs = { (LoadBalancingConfig)new RoundRobinConfig() }
            };
        });
    }
}
