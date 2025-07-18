using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            var email = User.FindFirstValue(ClaimTypes.Email)
                      ?? User.FindFirstValue(JwtRegisteredClaimNames.Email);

            return Ok(new
            {
                UserId = userId,
                Email = email
            });
        }
    }
}
