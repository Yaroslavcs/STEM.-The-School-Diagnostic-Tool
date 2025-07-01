namespace project.Database.Models
{
    public class Response
    {
        public int ResponseID { get; set; }
        public int StudentID { get; set; }
        public User Student { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
        public int AnswerValue { get; set; } // 1-5 або 1-7
    }
} 