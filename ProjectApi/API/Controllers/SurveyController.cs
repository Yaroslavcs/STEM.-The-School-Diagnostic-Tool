using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Database.Data;
using ProjectApi.Database.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Студент отримує активний опитувальник
        [Authorize(Roles = "Student")]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveSurvey()
        {
            var survey = await _context.Surveys
                .Include(s => s.Questions)
                .OrderByDescending(s => s.DateCreated)
                .FirstOrDefaultAsync();
            return Ok(survey);
        }

        // Студент надсилає відповіді
        [Authorize(Roles = "Student")]
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitResponses([FromBody] Response[] responses)
        {
            _context.Responses.AddRange(responses);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Завуч/Директор бачить середнє по класу
        [Authorize(Roles = "Deputy,Director")]
        [HttpGet("class-average/{schoolId}/{grade}")]
        public async Task<IActionResult> GetClassAverage(int schoolId, int grade)
        {
            var school = await _context.Schools.FindAsync(schoolId);
            if (school == null) return NotFound();

            var students = await _context.Users
                .Where(u => u.SchoolId == schoolId && u.Role != null && u.Role.Name == "Student" && u.Grade == grade)
                .Select(u => u.Id)
                .ToListAsync();

            if (students == null || students.Count == 0) return NotFound();

            var responses = await _context.Responses
                .Where(r => students.Contains(r.StudentID))
                .Include(r => r.Question)
                .ToListAsync();

            var result = responses
                .Where(r => r.Question != null && r.Question.STEMLetter != null)
                .GroupBy(r => r.Question!.STEMLetter!)
                .Select(g => new
                {
                    STEMLetter = g.Key,
                    Average = g.Average(x => x.AnswerValue)
                });

            return Ok(result);
        }

        // Директор бачить всі відповіді учнів по школі
        [Authorize(Roles = "Director")]
        [HttpGet("school-responses/{schoolId}")]
        public async Task<IActionResult> GetSchoolResponses(int schoolId)
        {
            var students = await _context.Users
                .Where(u => u.SchoolId == schoolId && u.Role != null && u.Role.Name == "Student")
                .Select(u => u.Id)
                .ToListAsync();

            var responses = await _context.Responses
                .Where(r => students.Contains(r.StudentID))
                .Include(r => r.Question)
                .ToListAsync();

            return Ok(responses);
        }

        // Учень бачить свої відповіді
        [Authorize(Roles = "Student")]
        [HttpGet("my-responses")]
        public async Task<IActionResult> GetMyResponses()
        {
            var nameIdClaim = User.FindFirst("nameidentifier");
            if (nameIdClaim == null) return Unauthorized();
            var userId = int.Parse(nameIdClaim.Value);
            var responses = await _context.Responses
                .Where(r => r.StudentID == userId)
                .Include(r => r.Question)
                .ToListAsync();
            return Ok(responses);
        }

        // Середній бал учня по STEM
        [Authorize(Roles = "Student")]
        [HttpGet("my-average")]
        public async Task<IActionResult> GetMyAverage()
        {
            var nameIdClaim = User.FindFirst("nameidentifier");
            if (nameIdClaim == null) return Unauthorized();
            var userId = int.Parse(nameIdClaim.Value);
            var responses = await _context.Responses
                .Where(r => r.StudentID == userId)
                .Include(r => r.Question)
                .ToListAsync();

            var result = responses
                .Where(r => r.Question != null && r.Question.STEMLetter != null)
                .GroupBy(r => r.Question!.STEMLetter!)
                .Select(g => new
                {
                    STEMLetter = g.Key,
                    Average = g.Average(x => x.AnswerValue)
                });

            return Ok(result);
        }
    }
} 