using System.Collections.Generic;

namespace ProjectApi.Database.Models {
    public class Recommendation {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int Grade { get; set; }
        public bool IsViewed { get; set; }
        public string? Note { get; set; }
        public int StudentId { get; set; }
        public User? Student { get; set; }
        public int? TeacherId { get; set; }
        public User? Teacher { get; set; }
    }
}
