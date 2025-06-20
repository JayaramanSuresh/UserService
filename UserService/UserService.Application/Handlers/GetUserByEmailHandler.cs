using MediatR;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Domain.Repositories;

namespace UserService.Application.Handlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto?>
    {
        private readonly IUserRepository _repo;
        public GetUserByEmailHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserDto?> Handle(GetUserByEmailQuery q, CancellationToken ct)
        {
            var user = await _repo.GetByEmailAsync(q.Email, ct);
            if (user == null) return null;

            return new UserDto(user.Id, user.Email, user.FullName, user.CreatedAt);
        }
    }
}
