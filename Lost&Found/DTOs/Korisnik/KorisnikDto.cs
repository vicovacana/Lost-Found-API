namespace Lost_Found.DTOs.Korisnik
{
    public class KorisnikDto
    {
        public int KorisnikId { get; set; }
        public string KorisnickoIme { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime VremeKreiranja { get; set; }
        public string Uloga { get; set; } = string.Empty;
    }
}
