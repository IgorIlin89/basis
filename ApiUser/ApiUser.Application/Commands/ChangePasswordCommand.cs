using ApiUser.Domain.Dtos;

namespace ApiUser.Application.Commands;

public record ChangePasswordCommand
{
    public int Id { get; init; }
    public string Password { get; init; }
    public ChangePasswordCommand(ChangePasswordDto changePasswordDto)
    {
        Id = changePasswordDto.UserId;
        Password = changePasswordDto.Password;
    }
}
