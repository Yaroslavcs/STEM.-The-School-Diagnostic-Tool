namespace project.Database.Models
{
    public class StudentGrade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public double Grade { get; set; }
    }
} 