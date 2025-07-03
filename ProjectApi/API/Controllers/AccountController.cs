using Microsoft.AspNetCore.Mvc;
using ProjectApi.Bussines_Logic.Services;
using ProjectApi.API.DTOs;
using System.Threading.Tasks;

namespace ProjectApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var result = await _service.ChangePasswordAsync(dto);
            if (!result) return BadRequest("Неправильний старий пароль або користувача не знайдено.");
            return Ok("Пароль змінено успішно.");
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromBody] ResetPasswordRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email)) return BadRequest("Email is required");
            var token = await _service.GenerateResetTokenAsync(dto.Email);
            if (token == null) return NotFound();
            return Ok(token);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _service.ResetPasswordAsync(dto);
            if (!result) return BadRequest("Невірний токен або email.");
            return Ok("Пароль скинуто успішно.");
        }
    }
} 