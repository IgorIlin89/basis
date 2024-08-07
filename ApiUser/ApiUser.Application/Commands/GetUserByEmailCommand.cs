using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiUser.Domain.Exceptions;

namespace ApiUser.Application.Commands;

public record GetUserByEmailCommand
{
    public string Email { get; init; }
    public GetUserByEmailCommand(string email)
    {
        if(email is null)
        {
            throw new NotFoundException($"Email should not be null when searching for user by emaul");
        }

        Email = email;
    }
}
