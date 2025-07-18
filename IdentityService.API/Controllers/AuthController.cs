using MediatR;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Application.DTOs;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
