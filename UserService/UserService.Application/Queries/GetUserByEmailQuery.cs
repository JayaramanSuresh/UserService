using MediatR;
using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto?>;
}
