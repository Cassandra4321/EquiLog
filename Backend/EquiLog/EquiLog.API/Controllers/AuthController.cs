using EquiLog.Contracts.Auth;
using EquiLog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EquiLog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            try
            {
                var result = await _authService.LoginAsync(loginRequest);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password");
            }
        }

    }
}
