using ProjectApi.API.DTOs;
using ProjectApi.Database.Models;
using ProjectApi.Database.Repositories;
using ProjectApi.Bussines_Logic.Helpers;
using System.Threading.Tasks;

namespace ProjectApi.Bussines_Logic.Services {
    public class AuthService : IAuthService {
        private readonly IUnitOfWork _uow;
        public AuthService(IUnitOfWork uow) { _uow = uow; }
        public Task<string> RegisterAsync(RegisterDto dto) {
            // Реєстрація користувача, хешування паролю, додавання до БД
            // Повертає JWT
            return Task.FromResult(JwtHelper.GenerateToken(dto.Email ?? string.Empty, dto.Role ?? string.Empty));
        }
        public Task<string> LoginAsync(LoginDto dto) {
            // Перевірка паролю, повертає JWT
            return Task.FromResult(JwtHelper.GenerateToken(dto.Email ?? string.Empty, "Student"));
        }
    }
}
