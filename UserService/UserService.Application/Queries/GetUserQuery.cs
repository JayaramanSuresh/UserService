using MediatR;
using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetUserQuery(Guid Id) : IRequest<UserDto?>;
}
