using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Domain.Exceptions;

public class UserUpdateException :Exception
{
    public UserUpdateException(string message)
        : base(message)
    {

    }

    public UserUpdateException(string message, Exception exception)
    : base(message, exception)
    {

    }
}
