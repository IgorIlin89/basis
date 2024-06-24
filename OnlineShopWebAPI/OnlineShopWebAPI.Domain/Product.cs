using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWebAPI.Domain;

public class Product
{
    public int Id { get; set; }
    public int Name { get; set; }
    public string Producer { get; set; }
}
