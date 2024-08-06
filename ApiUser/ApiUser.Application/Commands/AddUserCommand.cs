using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUser.Application.Commands;

public record AddUserCommand(string email, string name)
{
}
