using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain.Exceptions;

public class DomainException : Exception
{
	public DomainException(string message)
		:base(message)
	{

	}

    public DomainException(string message, Exception exception)
    : base(message, exception)
    {

    }
}
