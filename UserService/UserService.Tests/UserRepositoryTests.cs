using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Repositories;
using Xunit;

namespace UserService.Tests
{
    public class UserRepositoryTests
    {
        private UserDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "UserTestDb")
                .Options;

            return new UserDbContext(options);
        }

        [Fact]
        public async Task AddAndGetUserTest()
        {
            var context = GetDbContext();
            var repo = new UserRepository(context);

            var user = new User(Guid.NewGuid(), "test@domain.com", "Test User");

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var fetched = await repo.GetByEmailAsync("test@domain.com");
            Assert.IsNotNull(fetched);
            Assert.AreEqual("Test User", fetched?.FullName);
        }
    }
}
