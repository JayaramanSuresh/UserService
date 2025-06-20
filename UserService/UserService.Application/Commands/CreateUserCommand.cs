using MediatR;
using UserService.Application.Dtos;

namespace UserService.Application.Commands
{
    public record CreateUserCommand(string Email, string FullName) : IRequest<UserDto>;
}
