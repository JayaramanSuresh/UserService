using MediatR;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Domain.Repositories;

namespace UserService.Application.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto?>
    {
        private readonly IUserRepository _repo;
        public GetUserHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserDto?> Handle(GetUserQuery q, CancellationToken ct)
        {
            var user = await _repo.GetByIdAsync(q.Id, ct);
            if (user == null) return null;

            return new UserDto(user.Id, user.Email, user.FullName, user.CreatedAt);
        }
    }
}
