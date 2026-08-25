using System.ComponentModel.DataAnnotations;

namespace Lost_Found.DTOs.Korisnik
{
    public class KorisnikUpdateDto
    {
        [Required, MaxLength(50)]
        public string KorisnickoIme { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;
    }
}
