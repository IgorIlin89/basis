using Microsoft.Data.SqlClient;
using NServiceBus;
using OnlineShopWeb.Messages.V2.Events;

namespace OnlineShopWeb.Configuration;

public static class NServiceBusConfiguration
{
    public static async Task<IEndpointInstance> ConfigureNServiceBus(IServiceCollection services,
        IConfiguration configuration)
    {
        //This is name in Database
        var endpointConfiguration = new EndpointConfiguration("OnlineShopWeb");
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.EnableInstallers();

        // Choose JSON to serialize and deserialize messages
        endpointConfiguration.UseSerialization<NServiceBus.SystemJsonSerializer>();

        var nserviceBusConnectionString = configuration.GetConnectionString("NServiceBus");

        var transportConfig = new NServiceBus.SqlServerTransport(nserviceBusConnectionString)
        {
            DefaultSchema = "dbo",
            TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive,
            Subscriptions =
            {
                CacheInvalidationPeriod = TimeSpan.FromMinutes(1),
                SubscriptionTableName = new NServiceBus.Transport.SqlServer.SubscriptionTableName(
                    table: "Subscriptions",
                    schema: "dbo")
            }
        };

        transportConfig.SchemaAndCatalog.UseSchemaForQueue("error", "dbo");
        transportConfig.SchemaAndCatalog.UseSchemaForQueue("audit", "dbo");

        var transport = endpointConfiguration.UseTransport<SqlServerTransport>(transportConfig);
        transport.RouteToEndpoint(typeof(AddTransactionEvent), "ApiTransaction");

        //persistence
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");
        persistence.ConnectionBuilder(() => new SqlConnection(nserviceBusConnectionString));
        persistence.TablePrefix("");

        //await SqlServerHelper.CreateSchema(nserviceBusConnectionString, "dbo");

        var endpointContainer = EndpointWithExternallyManagedContainer.Create(endpointConfiguration,
            services);
        var endpointInstance = await endpointContainer.Start(services.BuildServiceProvider());

        services.AddSingleton<NServiceBus.IMessageSession>(endpointInstance);

        return endpointInstance;
    }
}
