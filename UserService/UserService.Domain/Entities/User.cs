namespace UserService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User() { }

        public User(Guid id, string email, string fullName)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email", nameof(email));
            Email = email;
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
