namespace Lost_Found.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public int KorisnikId { get; set; }
        public string KorisnickoIme { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Uloga { get; set; } = string.Empty;
    }
}
