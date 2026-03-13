using Microsoft.AspNetCore.Mvc;
using FarmerApp.Api.DTOs;
using FarmerApp.Api.Services;
using FarmerApp.Api.Utils;

namespace FarmerApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly IConfiguration _config;

        public AuthController(AuthService auth, IConfiguration config)
        {
            _auth = auth;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _auth.Register(dto);
            if (user == null)
                return BadRequest("User already exists");

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _auth.Login(dto);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
                return StatusCode(500, "JWT key is not configured");

            var token = JwtTokenGenerator.Generate(user, jwtKey);

            return Ok(new { token });
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("DB Connected");
        }
    }
}
