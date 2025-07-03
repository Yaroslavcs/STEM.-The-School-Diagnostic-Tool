using ProjectApi.Database.Models;
using ProjectApi.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ProjectApi.Database.Repositories {
    public class SchoolRepository : Repository<School>, ISchoolRepository {
        public SchoolRepository(ApplicationDbContext context) : base(context) { }
        public async Task<School?> GetByNameAsync(string name) =>
            await _context.Schools.FirstOrDefaultAsync(s => s.Name == name);
        public new async Task<IEnumerable<School>> GetAllAsync() =>
            await _context.Schools.ToListAsync();
    }
} 