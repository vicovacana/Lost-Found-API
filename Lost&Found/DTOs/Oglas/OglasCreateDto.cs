using System.ComponentModel.DataAnnotations;
using Lost_Found.Models.Enums;

namespace Lost_Found.DTOs.Oglas
{
    public class OglasCreateDto
    {
        [Required, MaxLength(150)]
        public string Naziv { get; set; } = string.Empty;

        [Required, MaxLength(2000)]
        public string Opis { get; set; } = string.Empty;

        [Required]
        public TipOglasa Tip { get; set; }

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }

        [MaxLength(500)]
        public string? Fotografija { get; set; }
    }
}
