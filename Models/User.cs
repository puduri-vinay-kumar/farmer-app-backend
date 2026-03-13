using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FarmerApp.Api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Qualification { get; set; }

        public string District { get; set; }
        public string Block { get; set; }
        public string Ivcs { get; set; }
        public string Village { get; set; }

        public string Pincode { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }

        public string EpicOrAadhar { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
