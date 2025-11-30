using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        [HttpGet("token-validation")]
        [Authorize]
        public IActionResult Check()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { messagge = $"Token Valido, UserId: { userId } Role: {role}"});
        }

        [HttpGet("server-status")]
        public IActionResult ServerStatus()
        {
            return Ok(new
            {
                status = "Online",
                timestamp = DateTime.UtcNow
            });
        }
    }

}
