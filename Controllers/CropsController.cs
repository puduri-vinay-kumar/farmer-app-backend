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

        public CropsController(CropService cropService)
        {
            _cropService = cropService;
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
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("Invalid token");

            var crops = await _cropService.GetCropsByUserAsync(userId);
            return Ok(crops);
        }
    }
}
