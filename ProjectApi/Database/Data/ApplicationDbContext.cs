using Microsoft.EntityFrameworkCore;
using ProjectApi.Database.Models;

namespace ProjectApi.Database.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<ClassResult> ClassResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Student" },
                new Role { Id = 2, Name = "Teacher" },
                new Role { Id = 3, Name = "Deputy" },
                new Role { Id = 4, Name = "Director" },
                new Role { Id = 5, Name = "Admin" }
            );
            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Science" },
                new Subject { Id = 2, Name = "Technology" },
                new Subject { Id = 3, Name = "Engineering" },
                new Subject { Id = 4, Name = "Mathematics" }
            );
        }
    }
}
