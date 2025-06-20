using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(eb =>
            {
                eb.HasKey(u => u.Id);
                eb.Property(u => u.Email).IsRequired().HasMaxLength(320);
                eb.HasIndex(u => u.Email).IsUnique();
                eb.Property(u => u.FullName).IsRequired();
                eb.Property(u => u.CreatedAt).IsRequired();
            });
        }
    }
}
