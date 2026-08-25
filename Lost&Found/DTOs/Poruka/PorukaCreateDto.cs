using System.ComponentModel.DataAnnotations;

namespace Lost_Found.DTOs.Poruka
{
    public class PorukaCreateDto
    {
        [Required, MaxLength(4000)]
        public string Sadrzaj { get; set; } = string.Empty;
    }
}
