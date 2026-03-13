namespace FarmerApp.Api.DTOs
{
    public class UserProfileDto
    {
        public string? Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime Dob { get; set; }

        public string Qualification { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string Block { get; set; } = string.Empty;

        public string Ivcs { get; set; } = string.Empty;

        public string Village { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;

        public string CurrentAddress { get; set; } = string.Empty;

        public string PermanentAddress { get; set; } = string.Empty;

        public string EpicOrAadhar { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
