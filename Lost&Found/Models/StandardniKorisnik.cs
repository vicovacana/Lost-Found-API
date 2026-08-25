namespace Lost_Found.Models
{
    public class StandardniKorisnik : Korisnik
    {
        public ICollection<Oglas> Oglasi { get; set; } = new List<Oglas>();
        public ICollection<Potrazivanje> Potrazivanja { get; set; } = new List<Potrazivanje>();
    }
}
