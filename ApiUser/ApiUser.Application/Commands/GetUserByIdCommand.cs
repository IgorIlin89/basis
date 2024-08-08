using ApiUser.Domain.Exceptions;
namespace ApiUser.Application.Commands;

public record GetUserByIdCommand
{
    public string Id { get; init; }
    public GetUserByIdCommand(string id)
    {
        if (id is null)
        {
            throw new NotFoundException($"The id may not be null when calling 'GetUserById'");
        }

        Id = id;
    }
}
