using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public EProductCategorys Category { get; set; }
    public string Picture { get; set; }
    //public List<string> Tags { get; set; }
}
