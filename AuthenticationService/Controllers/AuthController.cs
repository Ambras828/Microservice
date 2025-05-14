using AuthenticationService.Models;
using AuthenticationService.Services;
using Infrastructure.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
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
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            if (!_authService.ValidateUser(userLogin.Username, userLogin.Password))
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("Invalid credentials"));
            }

            var token = _authService.GenerateToken(userLogin.Username);
            return Ok(ApiResponse<string>.SuccessResponse(token, "Login successful"));
        }
    }
}
