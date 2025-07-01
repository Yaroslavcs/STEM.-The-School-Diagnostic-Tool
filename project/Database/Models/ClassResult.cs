namespace project.Database.Models
{
    public class ClassResult
    {
        public int ClassResultID { get; set; }
        public int ClassSurveyID { get; set; }
        public Survey Survey { get; set; }
        public string STEMLetter { get; set; }
        public double AverageScore { get; set; }
        public int SchoolId { get; set; }
        public int Grade { get; set; }
    }
} 