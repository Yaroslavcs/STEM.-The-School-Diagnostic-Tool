using System;
using System.Collections.Generic;

namespace ProjectApi.Database.Models
{
    public class Survey
    {
        public int SurveyID { get; set; }
        public string? Title { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}