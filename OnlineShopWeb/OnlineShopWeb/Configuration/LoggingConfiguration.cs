using Serilog;

namespace OnlineShopWeb.Configuration;

public static class LoggingConfiguration
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services,
        IConfiguration configuration,
        IHostBuilder hostBuilder)
    {
        var loggingConfiguration = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProcessId()
        .Enrich.WithProcessName()
        .Enrich.WithMachineName();

        var logger = loggingConfiguration.CreateLogger();
        hostBuilder.UseSerilog(logger);

        return services;
    }
}
