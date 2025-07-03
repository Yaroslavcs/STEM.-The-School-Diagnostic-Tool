using ProjectApi.API.DTOs;
using ProjectApi.Database.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectApi.Bussines_Logic.Services {
    public interface ISchoolService {
        Task<School> CreateAsync(SchoolDto dto);
        Task<IEnumerable<School>> GetAllAsync();
    }
} 