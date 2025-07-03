using System.ComponentModel.DataAnnotations;

namespace ProjectApi.API.DTOs
{
    public class ResetPasswordRequestDto
    {
        [Required]
        public string? Email { get; set; }
    }
} 