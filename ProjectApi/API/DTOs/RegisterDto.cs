using System.ComponentModel.DataAnnotations;

namespace ProjectApi.API.DTOs {
    public class RegisterDto {
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public int SchoolId { get; set; }
    }
}
