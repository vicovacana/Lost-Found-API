using System.ComponentModel.DataAnnotations;

namespace Lost_Found.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string KorisnickoIme { get; set; } = string.Empty;

        [Required]
        public string Lozinka { get; set; } = string.Empty;
    }
}
