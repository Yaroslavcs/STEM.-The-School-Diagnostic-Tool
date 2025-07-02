namespace ProjectApi.API.DTOs
{
    public class ClassResultDto
    {
        public int ClassResultID { get; set; }
        public int ClassSurveyID { get; set; }
        public string? STEMLetter { get; set; }
        public double AverageScore { get; set; }
        public int SchoolId { get; set; }
        public int Grade { get; set; }
    }
} 