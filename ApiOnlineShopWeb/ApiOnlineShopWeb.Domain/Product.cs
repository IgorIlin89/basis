using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public ProductCategory Category { get; set; }
    public string Picture { get; set; }
    public decimal Price { get; set; }
}
