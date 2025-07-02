using ProjectApi.Database.Models;
using System.Threading.Tasks;

namespace ProjectApi.Database.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
} 