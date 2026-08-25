using Lost_Found.DTOs.Poruka;

namespace Lost_Found.Services
{
    public interface IPorukaService
    {
        Task<IReadOnlyList<PorukaDto>> GetForRazgovorAsync(int razgovorId, int currentKorisnikId, bool isAdmin);
        Task<PorukaDto> CreateAsync(int razgovorId, int korisnikId, bool isAdmin, PorukaCreateDto dto);
    }
}
