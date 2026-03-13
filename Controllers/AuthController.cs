using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            var phone = User.FindFirstValue("phone");

            if (string.IsNullOrWhiteSpace(userId) && string.IsNullOrWhiteSpace(phone))
                return Unauthorized("Invalid token");

            var user = await _auth.GetProfile(userId ?? string.Empty, phone);
            if (user == null)
                return NotFound("User not found");

            return Ok(new UserProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Phone = user.Phone,
                Gender = user.Gender,
                Dob = user.Dob,
                Qualification = user.Qualification,
                District = user.District,
                Block = user.Block,
                Ivcs = user.Ivcs,
                Village = user.Village,
                Pincode = user.Pincode,
                CurrentAddress = user.CurrentAddress,
                PermanentAddress = user.PermanentAddress,
                EpicOrAadhar = user.EpicOrAadhar,
                CreatedAt = user.CreatedAt
            });
        }
    }
}
