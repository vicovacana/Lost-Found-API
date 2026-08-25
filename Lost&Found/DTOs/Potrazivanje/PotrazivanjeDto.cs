using Lost_Found.Models.Enums;

namespace Lost_Found.DTOs.Potrazivanje
{
    public class PotrazivanjeDto
    {
        public int KorisnikId { get; set; }
        public string KorisnickoIme { get; set; } = string.Empty;
        public int OglasId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public StatusPotrazivanja Status { get; set; }
        public DateTime? DatumRazresavanja { get; set; }
    }
}
