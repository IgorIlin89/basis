﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public  class ProductInCart
{
    public int Id { get; set; }
    public int Count { get; set; }
    //public ICollection<Product>? ProductsInCart { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int TransactionHistoryId { get; set; }
    public TransactionHistory TransactionHistory { get; set; }

}
