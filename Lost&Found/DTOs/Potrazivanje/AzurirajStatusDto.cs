using System.ComponentModel.DataAnnotations;
using Lost_Found.Models.Enums;

namespace Lost_Found.DTOs.Potrazivanje
{
    public class AzurirajStatusDto
    {
        [Required]
        public StatusPotrazivanja Status { get; set; }
    }
}
