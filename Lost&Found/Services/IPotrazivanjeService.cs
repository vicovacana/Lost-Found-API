using Lost_Found.DTOs.Potrazivanje;
using Lost_Found.Models.Enums;

namespace Lost_Found.Services
{
    public interface IPotrazivanjeService
    {
        Task<PotrazivanjeDto> CreateAsync(int oglasId, int korisnikId);
        Task<IReadOnlyList<PotrazivanjeDto>> GetForOglasAsync(int oglasId, int currentKorisnikId, bool isAdmin);
        Task<IReadOnlyList<PotrazivanjeDto>> GetMineAsync(int korisnikId);
        Task<PotrazivanjeDto> UpdateStatusAsync(int oglasId, int korisnikId, StatusPotrazivanja noviStatus);
        Task WithdrawAsync(int oglasId, int korisnikId, int currentKorisnikId);
    }
}
