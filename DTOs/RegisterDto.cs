using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.DTOs
{
    public class RegisterDto
    {
        [Required] public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required] public string LastName { get; set; }

        [Required, RegularExpression(@"^\d{10}$")]
        public string Phone { get; set; }

        public string Gender { get; set; }

        [Required] public DateTime Dob { get; set; }

        public string Qualification { get; set; }

        [Required] public string District { get; set; }
        [Required] public string Block { get; set; }

        public string Ivcs { get; set; }
        public string Village { get; set; }

        [Required] public string Pincode { get; set; }

        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }

        public string EpicOrAadhar { get; set; }

        [Required] public string Password { get; set; }
    }
}