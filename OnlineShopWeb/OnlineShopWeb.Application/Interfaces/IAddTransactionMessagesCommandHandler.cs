﻿using OnlineShopWeb.Application.Commands.Transaction;

namespace OnlineShopWeb.Application.Interfaces;

public interface IAddTransactionMessagesCommandHandler
{
    void Handle(AddTransactionCommandHttp command, CancellationToken cancellationToken);
}