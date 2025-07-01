using System.Collections.Generic;

namespace project.Database.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public int SurveyID { get; set; }
        public Survey Survey { get; set; }
        public string STEMLetter { get; set; } // S, T, E, M
        public ICollection<Response> Responses { get; set; }
    }
} 