using FarmerApp.Api.Data;
using FarmerApp.Api.DTOs;
using FarmerApp.Api.Models;
using FarmerApp.Api.Utils;
using MongoDB.Driver;

namespace FarmerApp.Api.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> Register(RegisterDto dto)
        {
            var existingUser = await _db.Users
                .Find(u => u.Phone == dto.Phone)
                .FirstOrDefaultAsync();

            if (existingUser != null)
                return null;

            var user = new User
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Gender = dto.Gender,
                Dob = dto.Dob,
                Qualification = dto.Qualification,
                District = dto.District,
                Block = dto.Block,
                Ivcs = dto.Ivcs,
                Village = dto.Village,
                Pincode = dto.Pincode,
                CurrentAddress = dto.CurrentAddress,
                PermanentAddress = dto.PermanentAddress,
                EpicOrAadhar = dto.EpicOrAadhar,
                PasswordHash = PasswordHelper.Hash(dto.Password)
            };

            await _db.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<User?> Login(LoginDto dto)
        {
            var user = await _db.Users
                .Find(u => u.Phone == dto.Phone)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            return PasswordHelper.Verify(dto.Password, user.PasswordHash)
                ? user
                : null;
        }

        public async Task<User?> GetProfile(string userId, string? phone)
        {
            var user = await _db.Users
                .Find(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user != null || string.IsNullOrWhiteSpace(phone))
                return user;

            return await _db.Users
                .Find(u => u.Phone == phone)
                .FirstOrDefaultAsync();
        }
    }
}
