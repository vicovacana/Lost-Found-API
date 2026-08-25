using System.ComponentModel.DataAnnotations;
using Lost_Found.Models.Enums;

namespace Lost_Found.DTOs.Razgovor
{
    public class AzurirajStatusRazgovoraDto
    {
        [Required]
        public StatusRazgovora StatusRazgovora { get; set; }
    }
}
