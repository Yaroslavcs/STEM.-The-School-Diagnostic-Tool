using ProjectApi.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Database.Repositories
{
    public interface IRecommendationRepository : IRepository<Recommendation>
    {
        Task<IEnumerable<Recommendation>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<Recommendation>> GetByTeacherIdAsync(int teacherId);
    }
} 