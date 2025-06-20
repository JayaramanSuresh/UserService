using MediatR;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _repo;
        public CreateUserHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserDto> Handle(CreateUserCommand cmd, CancellationToken ct)
        {
            var existingUser = await _repo.GetByEmailAsync(cmd.Email, ct);
            if (existingUser != null)
                throw new InvalidOperationException("Email already registered.");

            var user = new User(Guid.NewGuid(), cmd.Email, cmd.FullName);
            await _repo.AddAsync(user, ct);
            await _repo.SaveChangesAsync(ct);

            return new UserDto(user.Id, user.Email, user.FullName, user.CreatedAt);
        }
    }
}
