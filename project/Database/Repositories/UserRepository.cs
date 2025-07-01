using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;
namespace Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        public async Task<User> GetByEmailAsync(string email) =>
            await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }
}
