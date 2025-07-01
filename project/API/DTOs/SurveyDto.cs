using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class SurveyDto
    {
        public int SurveyID { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
} 