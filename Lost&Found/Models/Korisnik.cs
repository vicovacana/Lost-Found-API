namespace Lost_Found.Models
{
    public abstract class Korisnik
    {
        public int KorisnikId { get; set; }
        public string KorisnickoIme { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LozinkaHash { get; set; } = string.Empty;
        public DateTime VremeKreiranja { get; set; }

        public ICollection<Poruka> Poruke { get; set; } = new List<Poruka>();
    }
}
