using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}