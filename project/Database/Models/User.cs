using System.Collections.Generic;

namespace project.Database.Models {
    public class User {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public int Grade { get; set; }
        public string ResetToken { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}
