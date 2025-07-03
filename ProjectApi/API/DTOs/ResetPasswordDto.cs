using System.ComponentModel.DataAnnotations;

namespace ProjectApi.API.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? NewPassword { get; set; }
        [Required]
        public string? ResetToken { get; set; }
    }
} 