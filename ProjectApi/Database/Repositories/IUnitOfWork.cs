using System.Threading.Tasks;

namespace ProjectApi.Database.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRecommendationRepository Recommendations { get; }
        ISchoolRepository Schools { get; }
        IRepository<ProjectApi.Database.Models.Role> Roles { get; }
        Task<int> CompleteAsync();
    }
} 