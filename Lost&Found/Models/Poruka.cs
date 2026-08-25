namespace Lost_Found.Models
{
    public class Poruka
    {
        public int PorukaId { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; } = null!;

        public int RazgovorId { get; set; }
        public Razgovor Razgovor { get; set; } = null!;

        public DateTime DatumKreiranja { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;
    }
}
