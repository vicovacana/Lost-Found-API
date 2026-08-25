using Lost_Found.Models.Enums;

namespace Lost_Found.Models
{
    public class Potrazivanje
    {
        public int KorisnikId { get; set; }
        public StandardniKorisnik Korisnik { get; set; } = null!;

        public int OglasId { get; set; }
        public Oglas Oglas { get; set; } = null!;

        public DateTime DatumKreiranja { get; set; }
        public StatusPotrazivanja Status { get; set; }
        public DateTime? DatumRazresavanja { get; set; }
    }
}
