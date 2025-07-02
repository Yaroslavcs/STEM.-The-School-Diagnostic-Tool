using System.Threading.Tasks;

namespace ProjectApi.Database.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRecommendationRepository Recommendations { get; }
        Task<int> CompleteAsync();
    }
} 