using MediatR;
using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetUsersPagedQuery(int PageNumber, int PageSize) : IRequest<List<UserDto>>;
}
