using ProjectApi.Database.Data;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Database.Models;
using System.Threading.Tasks;
using ProjectApi.Database.Repositories;

namespace ProjectApi.Database.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }
}
