using ProjectApi.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Bussines_Logic.Services;
using Microsoft.OpenApi.Models;

namespace ProjectApi.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) { _authService = authService; }
        /// <summary>
        /// Реєстрація користувача
        /// </summary>
        /// <param name="name">Ім'я користувача</param>
        /// <param name="email">Введіть коректний email (має містити @)</param>
        /// <param name="password">Пароль (мінімум 16 символів)</param>
        /// <param name="role">Роль (Admin, Director, Teacher, Student)</param>
        /// <param name="schoolId">Id школи</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromQuery] string name,
            [FromQuery, System.ComponentModel.DataAnnotations.EmailAddress] string email,
            [FromQuery] string password,
            [FromQuery] string role,
            [FromQuery] int schoolId)
        {
            var dto = new RegisterDto {
                Name = name,
                Email = email,
                Password = password,
                Role = role,
                SchoolId = schoolId
            };
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { token });
        }
        /// <summary>
        /// Вхід користувача
        /// </summary>
        /// <param name="email">Email користувача</param>
        /// <param name="password">Пароль користувача</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromQuery] string email,
            [FromQuery] string password)
        {
            var dto = new LoginDto { Email = email, Password = password };
            var token = await _authService.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
