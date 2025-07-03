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

        /// <summary>
        /// Зміна пароля користувача
        /// </summary>
        /// <param name="userId">ID користувача</param>
        /// <param name="oldPassword">Старий пароль</param>
        /// <param name="newPassword">Новий пароль</param>
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromQuery] int userId,
            [FromQuery] string oldPassword,
            [FromQuery] string newPassword)
        {
            var dto = new ChangePasswordDto { UserId = userId, OldPassword = oldPassword, NewPassword = newPassword };
            var result = await _service.ChangePasswordAsync(dto);
            if (!result) return BadRequest("Неправильний старий пароль або користувача не знайдено.");
            return Ok("Пароль змінено успішно.");
        }

        /// <summary>
        /// Запит на скидання пароля (відправка токена на email)
        /// </summary>
        /// <param name="email">Email користувача</param>
        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Email is required");
            var token = await _service.GenerateResetTokenAsync(email);
            if (token == null) return NotFound();
            return Ok(token);
        }

        /// <summary>
        /// Скидання пароля за токеном
        /// </summary>
        /// <param name="email">Email користувача</param>
        /// <param name="newPassword">Новий пароль</param>
        /// <param name="resetToken">Токен для скидання</param>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            [FromQuery] string email,
            [FromQuery] string newPassword,
            [FromQuery] string resetToken)
        {
            var dto = new ResetPasswordDto { Email = email, NewPassword = newPassword, ResetToken = resetToken };
            var result = await _service.ResetPasswordAsync(dto);
            if (!result) return BadRequest("Невірний токен або email.");
            return Ok("Пароль скинуто успішно.");
        }
    }
} 