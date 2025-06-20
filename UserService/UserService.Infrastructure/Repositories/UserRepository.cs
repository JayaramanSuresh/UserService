using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _db;
        public UserRepository(UserDbContext db) => _db = db;

        public async Task AddAsync(User user, CancellationToken ct = default) =>
            await _db.Users.AddAsync(user, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
            await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);

        public async Task SaveChangesAsync(CancellationToken ct = default) =>
            await _db.SaveChangesAsync(ct);
    }
}
