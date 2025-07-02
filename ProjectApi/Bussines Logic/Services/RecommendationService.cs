using ProjectApi.API.DTOs;
using ProjectApi.Database.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Bussines_Logic.Services {
    public class RecommendationService : IRecommendationService {
        private readonly IUnitOfWork _uow;
        public RecommendationService(IUnitOfWork uow) { _uow = uow; }
        public Task<IEnumerable<RecommendationDto>> GetForStudentAsync(int studentId) { return Task.FromResult<IEnumerable<RecommendationDto>>(new List<RecommendationDto>()); }
        public Task<IEnumerable<RecommendationDto>> GetForTeacherAsync(int teacherId) { return Task.FromResult<IEnumerable<RecommendationDto>>(new List<RecommendationDto>()); }
        public Task MarkAsViewedAsync(int recommendationId) { return Task.CompletedTask; }
        public Task AddNoteAsync(int recommendationId, string note) { return Task.CompletedTask; }
    }
}
