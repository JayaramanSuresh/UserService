namespace UserService.Api.Interface
{
    public interface ITokenService
    {
        string BuildToken(string userId, string email);
    }
}
