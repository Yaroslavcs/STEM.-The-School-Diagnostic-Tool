using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using project.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace Bussines_Logic.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null) return false;

            var oldHash = HashPassword(dto.OldPassword);
            if (user.PasswordHash != oldHash) return false;

            user.PasswordHash = HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GenerateResetTokenAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            user.ResetToken = token;
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return false;
            if (user.ResetToken != dto.ResetToken) return false;

            user.PasswordHash = HashPassword(dto.NewPassword);
            user.ResetToken = null;
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return string.Join("", sha256.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(b => b.ToString("x2")));
        }
    }
} 