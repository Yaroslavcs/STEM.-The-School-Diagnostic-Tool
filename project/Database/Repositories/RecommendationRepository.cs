using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Repositories {
    public class RecommendationRepository : Repository<Recommendation>, IRecommendationRepository {
        public RecommendationRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Recommendation>> GetByStudentIdAsync(int studentId) =>
            await _context.Recommendations.Where(r => r.StudentId == studentId).ToListAsync();
        public async Task<IEnumerable<Recommendation>> GetByTeacherIdAsync(int teacherId) =>
            await _context.Recommendations.Where(r => r.TeacherId == teacherId).ToListAsync();
    }
}
