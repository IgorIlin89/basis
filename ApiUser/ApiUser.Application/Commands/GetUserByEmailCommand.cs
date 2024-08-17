using ApiUser.Domain.Exceptions;

namespace ApiUser.Application.Commands;

public record GetUserByEmailCommand
{
    public string Email { get; init; }
    public GetUserByEmailCommand(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new NotFoundException($"Email should not be null when searching for user by email");
        }

        Email = email;
    }
}
