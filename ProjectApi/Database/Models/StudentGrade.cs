namespace ProjectApi.Database.Models
{
    public class StudentGrade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public double Grade { get; set; }
    }
} 