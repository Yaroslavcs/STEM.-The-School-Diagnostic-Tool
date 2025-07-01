using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Database.Data;
using project.Database.Models;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
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
    }
} 