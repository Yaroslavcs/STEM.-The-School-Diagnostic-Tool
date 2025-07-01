using DTOs;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Services {
    public class RecommendationService : IRecommendationService {
        private readonly IUnitOfWork _uow;
        public RecommendationService(IUnitOfWork uow) { _uow = uow; }
        public async Task<IEnumerable<RecommendationDto>> GetForStudentAsync(int studentId) { return null; }
        public async Task<IEnumerable<RecommendationDto>> GetForTeacherAsync(int teacherId) { return null; }
        public async Task MarkAsViewedAsync(int recommendationId) { }
        public async Task AddNoteAsync(int recommendationId, string note) { }
    }
}
