using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain.Exceptions;

public class ApiException : Exception
{
	public ApiException(string message)
		:base(message)
	{

	}

    public ApiException(string message, Exception exception)
    : base(message, exception)
    {

    }
}
