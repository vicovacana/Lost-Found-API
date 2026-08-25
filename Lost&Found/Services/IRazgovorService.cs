using Lost_Found.DTOs.Razgovor;
using Lost_Found.Models.Enums;

namespace Lost_Found.Services
{
    public interface IRazgovorService
    {
        Task<RazgovorDto> OpenAsync(int oglasId);
        Task<RazgovorDto> GetForOglasAsync(int oglasId, int currentKorisnikId, bool isAdmin);
        Task<RazgovorDto> GetByIdAsync(int razgovorId, int currentKorisnikId, bool isAdmin);
        Task<RazgovorDto> UpdateStatusAsync(int razgovorId, StatusRazgovora noviStatus);

        // Reused by PorukeController to enforce the "admin, oglas owner, or a claimant" access rule.
        Task EnsureParticipantAsync(int razgovorId, int currentKorisnikId, bool isAdmin);
    }
}
