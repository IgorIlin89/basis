using ApiUser.Domain;
using ApiUser.Domain.Dtos;

namespace ApiUser.Application.Commands;

public record UpdateUserCommand
{
    public User User { get; init; }

    public UpdateUserCommand(UserDto userDto)
    {
        User = userDto.MapToUser();
    }
}
