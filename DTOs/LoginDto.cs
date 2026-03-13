using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.DTOs
{
    public class LoginDto
    {
        [Required] public string Phone { get; set; }
        [Required] public string Password { get; set; }
    }
}