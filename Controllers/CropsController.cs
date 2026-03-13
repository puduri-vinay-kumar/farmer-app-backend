using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FarmerApp.Api.DTOs;
using FarmerApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Api.Controllers
{
    [ApiController]
    [Route("api/crops")]
    public class CropsController : ControllerBase
    {
        private readonly CropService _cropService;
        private readonly ILogger<CropsController> _logger;

        public CropsController(CropService cropService, ILogger<CropsController> logger)
        {
            _cropService = cropService;
            _logger = logger;
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private void LogResolvedClaims(string endpointName)
        {
            var claims = User.Claims.Select(claim => $"{claim.Type}={claim.Value}").ToArray();
            _logger.LogInformation("Crop endpoint {Endpoint} claims: {Claims}", endpointName, claims);
            _logger.LogInformation("Crop endpoint {Endpoint} resolved userId: {UserId}", endpointName, GetUserId());
        }

        [HttpGet("catalog")]
        public IActionResult GetCatalog()
        {
            return Ok(_cropService.GetCatalog());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCrop(AddCropDto dto)
        {
            LogResolvedClaims("AddCrop");

            var userId = GetUserId();
            var userPhone = User.FindFirstValue("phone");

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(userPhone))
                return Unauthorized("Invalid token");

            var crop = await _cropService.CreateCropAsync(userId, userPhone, dto);

            return Ok(new
            {
                message = "Crop added successfully",
                crop
            });
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyCrops()
        {
            LogResolvedClaims("GetMyCrops");

            var userId = GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("Invalid token");

            var crops = await _cropService.GetCropsByUserAsync(userId);
            return Ok(crops);
        }
    }
}
