namespace Lost_Found.DTOs.Poruka
{
    public class PorukaDto
    {
        public int PorukaId { get; set; }
        public int KorisnikId { get; set; }
        public string KorisnickoIme { get; set; } = string.Empty;
        public int RazgovorId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;
    }
}
