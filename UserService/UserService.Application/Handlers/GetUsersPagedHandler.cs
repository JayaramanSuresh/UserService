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
    public class GetUsersPagedHandler : IRequestHandler<GetUsersPagedQuery, List<UserDto>>
    {
        private readonly IUserRepository _repo;

        public GetUsersPagedHandler(IUserRepository repo) => _repo = repo;

        public async Task<List<UserDto>> Handle(GetUsersPagedQuery request, CancellationToken ct)
        {
            var users = await _repo.GetPagedAsync(request.PageNumber, request.PageSize, ct);
            return users.Select(u => new UserDto(u.Id,u.FullName, u.Email, u.CreatedAt)).ToList();
        }
    }
}
