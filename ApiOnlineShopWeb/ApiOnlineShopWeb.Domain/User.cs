﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Domain;

public class User
{
    public int Id { get; set; }
    public string EMail { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}
