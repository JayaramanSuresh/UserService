using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Interface;
using UserService.Application.Queries;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public AuthController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(req.Email));
            if (user == null)
                return Unauthorized(new { error = "Invalid credentials" });

            // WARNING: Add password validation here in production!

            var token = _tokenService.BuildToken(user.Id.ToString(), user.Email);
            return Ok(new { token });
        }
    }

    public record LoginRequest(string Email);
}
