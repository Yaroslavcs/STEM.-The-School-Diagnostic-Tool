using ProjectApi.Database.Data;
using System.Threading.Tasks;
using ProjectApi.Database.Repositories;

namespace ProjectApi.Database.Repositories {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public IRecommendationRepository Recommendations { get; }
        public ISchoolRepository Schools { get; }
        public IRepository<ProjectApi.Database.Models.Role> Roles { get; }
        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
            Users = new UserRepository(_context);
            Recommendations = new RecommendationRepository(_context);
            Schools = new SchoolRepository(_context);
            Roles = new Repository<ProjectApi.Database.Models.Role>(_context);
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    }
}
