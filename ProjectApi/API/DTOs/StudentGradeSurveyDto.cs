using System.ComponentModel.DataAnnotations;

namespace ProjectApi.API.DTOs
{
    /// <summary>
    /// DTO для STEM-опитування студента
    /// </summary>
    public class StudentGradeSurveyDto
    {
        /// <summary>Email студента</summary>
        [Required]
        public string Email { get; set; } = string.Empty;
        /// <summary>Пароль студента</summary>
        [Required]
        public string Password { get; set; } = string.Empty;
        /// <summary>Назва предмету</summary>
        [Required]
        public string Subject { get; set; } = string.Empty;
        /// <summary>Для мене НАУКА — це (1-6)</summary>
        [Range(1,6)]
        public int Science { get; set; }
        /// <summary>Для мене МАТЕМАТИКА — це (1-6)</summary>
        [Range(1,6)]
        public int Math { get; set; }
        /// <summary>Для мене ІНЖЕНЕРІЯ — це (1-6)</summary>
        [Range(1,6)]
        public int Engineering { get; set; }
        /// <summary>Для мене ТЕХНОЛОГІЇ — це (1-6)</summary>
        [Range(1,6)]
        public int Technology { get; set; }
    }
} 