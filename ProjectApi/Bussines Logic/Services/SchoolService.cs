using ProjectApi.API.DTOs;
using ProjectApi.Database.Models;
using ProjectApi.Database.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectApi.Bussines_Logic.Services {
    public class SchoolService : ISchoolService {
        private readonly IUnitOfWork _uow;
        public SchoolService(IUnitOfWork uow) { _uow = uow; }
        public async Task<School> CreateAsync(SchoolDto dto) {
            var existing = await _uow.Schools.GetByNameAsync(dto.Name);
            if (existing != null) return existing;
            var school = new School { Name = dto.Name };
            await _uow.Schools.AddAsync(school);
            await _uow.CompleteAsync();
            return school;
        }
        public async Task<IEnumerable<School>> GetAllAsync() => await _uow.Schools.GetAllAsync();
    }
} 