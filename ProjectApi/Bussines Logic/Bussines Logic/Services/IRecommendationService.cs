using ProjectApi.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Bussines_Logic.Services
{
    public interface IRecommendationService
    {
        Task<IEnumerable<RecommendationDto>> GetForStudentAsync(int studentId);
        Task<IEnumerable<RecommendationDto>> GetForTeacherAsync(int teacherId);
        Task MarkAsViewedAsync(int recommendationId);
        Task AddNoteAsync(int recommendationId, string note);
    }
} 