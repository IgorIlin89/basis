using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Domain.Exceptions;

public class CouponExistsException : Exception
{
    public CouponExistsException(string message)
        : base(message)
    {

    }

    public CouponExistsException(string message, Exception exception)
        : base(message, exception)
    {

    }
}
