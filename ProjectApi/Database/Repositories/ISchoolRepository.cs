using ProjectApi.Database.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectApi.Database.Repositories {
    public interface ISchoolRepository : IRepository<School> {
        Task<School?> GetByNameAsync(string name);
        new Task<IEnumerable<School>> GetAllAsync();
    }
} 