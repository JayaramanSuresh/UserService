using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Dtos;
using UserService.Application.Queries;
using UserService.Domain.Repositories;

namespace UserService.Application.Handlers
{
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _repo;

        public GetAllUsersQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken ct)
        {
            var users = await _repo.GetAllAsync(ct);
            return users.Select(u => new UserDto(u.Id,u.FullName,u.Email,u.CreatedAt)).ToList();
        }
    }
}
