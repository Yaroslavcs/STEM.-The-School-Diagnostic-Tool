using System.ComponentModel.DataAnnotations;

namespace ProjectApi.API.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? OldPassword { get; set; }
        [Required]
        public string? NewPassword { get; set; }
    }
} 