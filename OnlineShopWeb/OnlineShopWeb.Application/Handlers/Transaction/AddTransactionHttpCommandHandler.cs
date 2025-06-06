﻿using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionHttpCommandHandler(ITransactionAdapter transactionAdapter)
    : IAddTransactionCommandHandler
{
    public async Task<Domain.Transaction> Handle(AddTransactionCommandHttp command,
        CancellationToken cancellationToken)
    {
        var result = await transactionAdapter.AddTransaction(command.UserId,
            command.ProductInCarts, command.Coupons);

        return result;
    }
}