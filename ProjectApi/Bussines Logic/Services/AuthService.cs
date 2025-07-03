using ProjectApi.API.DTOs;
using ProjectApi.Database.Models;
using ProjectApi.Database.Repositories;
using ProjectApi.Bussines_Logic.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;

namespace ProjectApi.Bussines_Logic.Services {
    public class AuthService : IAuthService {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        public AuthService(IUnitOfWork uow, IConfiguration configuration) { _uow = uow; _configuration = configuration; }
        public async Task<string> RegisterAsync(RegisterDto dto) {
            // Перевірка існування школи
            var school = await _uow.Schools.GetByIdAsync(dto.SchoolId);
            if (school == null)
                throw new Exception("School with this Id does not exist");
            // Перевірка існування ролі
            var roleEntity = (await _uow.Roles.GetAllAsync()).FirstOrDefault(r => r.Name == (dto.Role ?? "Student"));
            if (roleEntity == null)
                throw new Exception("Role does not exist");
            var user = await _uow.Users.GetByEmailAsync(dto.Email ?? string.Empty);
            if (user != null)
                throw new Exception("User already exists");
            var passwordHash = HashPassword(dto.Password ?? "");
            var userEntity = new User {
                Email = dto.Email,
                PasswordHash = passwordHash,
                FullName = dto.Name,
                RoleId = roleEntity.Id,
                SchoolId = dto.SchoolId
            };
            await _uow.Users.AddAsync(userEntity);
            await _uow.CompleteAsync();
            return JwtHelper.GenerateToken(userEntity.Email ?? "", roleEntity.Name ?? "Student", _configuration);
        }
        public async Task<string> LoginAsync(LoginDto dto) {
            var user = await _uow.Users.GetByEmailAsync(dto.Email ?? string.Empty);
            if (user == null || !VerifyPassword(dto.Password ?? "", user.PasswordHash ?? ""))
                throw new Exception("Invalid credentials");
            var role = user.Role?.Name ?? "Student";
            return JwtHelper.GenerateToken(user.Email ?? "", role, _configuration);
        }
        private static string HashPassword(string password) {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
        private static bool VerifyPassword(string password, string hash) {
            return HashPassword(password) == hash;
        }
    }
}
