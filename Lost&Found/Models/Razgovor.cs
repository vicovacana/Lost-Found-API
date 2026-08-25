using Lost_Found.Models.Enums;

namespace Lost_Found.Models
{
    public class Razgovor
    {
        public int RazgovorId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public StatusRazgovora StatusRazgovora { get; set; }

        public int OglasId { get; set; }
        public Oglas Oglas { get; set; } = null!;

        public ICollection<Poruka> Poruke { get; set; } = new List<Poruka>();
    }
}
