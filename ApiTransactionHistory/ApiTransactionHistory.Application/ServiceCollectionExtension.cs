using ApiTransactionHistory.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTransactionHistory.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAddTransactionHistoryCommandHandler, AddTransactionHistoryCommandHandler>()
            .AddScoped<IGetTransactionHistoryListCommandHandler, GetTransactionHistoryListCommandHandler>();

        return serviceCollection;
    }
}
