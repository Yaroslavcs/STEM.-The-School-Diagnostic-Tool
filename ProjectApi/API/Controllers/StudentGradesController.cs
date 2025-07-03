using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Database.Data;
using ProjectApi.Database.Models;
using ProjectApi.API.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentGradesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade([FromBody] StudentGrade grade)
        {
            _context.StudentGrades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok(grade);
        }

        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetGradesByStudent(int studentId)
        {
            var grades = await _context.StudentGrades
                .Include(g => g.Subject)
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
            return Ok(grades);
        }

        [HttpGet("average/{studentId}")]
        public async Task<IActionResult> GetAverageGrade(int studentId)
        {
            var avg = await _context.StudentGrades
                .Where(g => g.StudentId == studentId)
                .AverageAsync(g => g.Grade);
            return Ok(new { studentId, average = avg });
        }

        /// <summary>
        /// Надіслати STEM-опитування (оцінювання)
        /// </summary>
        /// <param name="model">Дані для STEM-опитування (email, password, subject, science, math, engineering, technology)</param>
        [HttpPost("survey")]
        public async Task<IActionResult> SubmitSurvey([FromBody] StudentGradeSurveyDto model)
        {
            // Валідація
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await Task.CompletedTask;
            // TODO: Перевірка email+password, пошук студента, предмету, збереження оцінок
            return Ok(new { message = "Відповіді збережено" });
        }

        /// <summary>
        /// Надіслати повну STEM-анкету (як на фото)
        /// </summary>
        /// <param name="model">Дані для повної анкети (ПІБ, клас, навчальний заклад, стать, всі шкали)</param>
        [HttpPost("survey-full")]
        public async Task<IActionResult> SubmitFullSurvey([FromBody] StudentSurveyFullDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await Task.CompletedTask;
            // TODO: Зберегти анкету в БД
            return Ok(new { message = "Анкету збережено" });
        }
    }
} 