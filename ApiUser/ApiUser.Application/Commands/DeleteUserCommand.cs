using ApiUser.Domain.Exceptions;
namespace ApiUser.Application.Commands;

public record DeleteUserCommand
{
    public string Id { get; init; }
    public DeleteUserCommand(string id)
    {
        if (id is null)
        {
            throw new NotFoundException($"No id passed to delete the appropriate user");
        }
        Id = id;
    }
}
