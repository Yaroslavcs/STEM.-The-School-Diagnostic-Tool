using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectApi.Bussines_Logic.Services;
using ProjectApi.API.DTOs;
using ProjectApi.Database.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectApi.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase {
        private readonly ISchoolService _schoolService;
        public SchoolsController(ISchoolService schoolService) { _schoolService = schoolService; }

        /// <summary>
        /// Додає нову школу (авторизація через токен у полі)
        /// </summary>
        /// <param name="token">JWT токен користувача (Admin/Director)</param>
        /// <param name="name">Назва школи</param>
        [HttpPost]
        public async Task<ActionResult<School>> Create(
            [FromQuery] string token,
            [FromQuery] string name)
        {
            // Тут має бути перевірка токена вручну (jwt validation)
            // ... (реалізую нижче)
            var dto = new SchoolDto { Name = name };
            var school = await _schoolService.CreateAsync(dto);
            return Ok(school);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Director")]
        public async Task<ActionResult<IEnumerable<School>>> GetAll() {
            var schools = await _schoolService.GetAllAsync();
            return Ok(schools);
        }

        /// <summary>
        /// Отримати список шкіл для вибору у формі анкети
        /// </summary>
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchoolList()
        {
            var schools = await _schoolService.GetAllAsync();
            return Ok(schools);
        }
    }
} 