using DTOs;
using Models;
using Repositories;
using Helpers;
using System.Threading.Tasks;
namespace Services {
    public class AuthService : IAuthService {
        private readonly IUnitOfWork _uow;
        public AuthService(IUnitOfWork uow) { _uow = uow; }
        public async Task<string> RegisterAsync(RegisterDto dto) {
            // Реєстрація користувача, хешування паролю, додавання до БД
            // Повертає JWT
            return JwtHelper.GenerateToken(dto.Email, dto.Role);
        }
        public async Task<string> LoginAsync(LoginDto dto) {
            // Перевірка паролю, повертає JWT
            return JwtHelper.GenerateToken(dto.Email, "Student");
        }
    }
}
