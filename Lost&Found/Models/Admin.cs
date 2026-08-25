namespace Lost_Found.Models
{
    public class Admin : Korisnik
    {
        public ICollection<Oglas> NadgledaniOglasi { get; set; } = new List<Oglas>();
    }
}
