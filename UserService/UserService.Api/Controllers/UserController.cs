using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Application.Queries;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand cmd)
        {
            var user = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id:guid}")]        
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(), ct);
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<List<UserDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            if (page <= 0 || size <= 0) return BadRequest("Invalid pagination values.");

            var result = await _mediator.Send(new GetUsersPagedQuery(page, size), ct);
            return Ok(result);
        }
    }
}
