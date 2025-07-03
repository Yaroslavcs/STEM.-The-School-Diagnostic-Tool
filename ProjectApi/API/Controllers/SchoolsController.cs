using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectApi.Bussines_Logic.Services;
using ProjectApi.API.DTOs;
using ProjectApi.Database.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectApi.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase {
        private readonly ISchoolService _schoolService;
        public SchoolsController(ISchoolService schoolService) { _schoolService = schoolService; }

        [HttpPost]
        [Authorize(Roles = "Admin,Director")]
        public async Task<ActionResult<School>> Create([FromBody] SchoolDto dto) {
            var school = await _schoolService.CreateAsync(dto);
            return Ok(school);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Director")]
        public async Task<ActionResult<IEnumerable<School>>> GetAll() {
            var schools = await _schoolService.GetAllAsync();
            return Ok(schools);
        }
    }
} 