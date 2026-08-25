using Lost_Found.DTOs.Korisnik;

namespace Lost_Found.Services
{
    public interface IKorisnikService
    {
        Task<IReadOnlyList<KorisnikDto>> GetAllAsync();
        Task<KorisnikDto> GetByIdAsync(int korisnikId);
        Task<KorisnikDto> UpdateAsync(int korisnikId, KorisnikUpdateDto dto);
        Task<KorisnikDto> CreateAdminAsync(KreirajAdminaDto dto);
        Task DeleteAsync(int korisnikId);
    }
}
