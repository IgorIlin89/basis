﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Domain.Exceptions;

public class UserExistsException : Exception
{
    int StatusCode { get; set; }
    public UserExistsException(string message)
        : base(message)
    {

    }

    public UserExistsException(string message, Exception exception)
    : base(message, exception)
    {

    }
}
