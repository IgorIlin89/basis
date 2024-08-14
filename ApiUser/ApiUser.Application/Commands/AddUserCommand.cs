using ApiUser.Domain;
using ApiUser.Domain.Dtos;
namespace ApiUser.Application.Commands;

public record AddUserCommand
{
    public User UserToAdd { get; init; }

    public AddUserCommand(DtoAddUser userDto)
    {
        UserToAdd = userDto.MapToUser();
    }
}
