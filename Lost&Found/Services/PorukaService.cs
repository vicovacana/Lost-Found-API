using Lost_Found.Common;
using Lost_Found.Data;
using Lost_Found.DTOs.Poruka;
using Lost_Found.Models;
using Lost_Found.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Lost_Found.Services
{
    public class PorukaService : IPorukaService
    {
        private readonly ApplicationDbContext _db;
        private readonly IRazgovorService _razgovorService;

        public PorukaService(ApplicationDbContext db, IRazgovorService razgovorService)
        {
            _db = db;
            _razgovorService = razgovorService;
        }

        public async Task<IReadOnlyList<PorukaDto>> GetForRazgovorAsync(int razgovorId, int currentKorisnikId, bool isAdmin)
        {
            await _razgovorService.EnsureParticipantAsync(razgovorId, currentKorisnikId, isAdmin);

            var poruke = await _db.Poruke
                .Include(p => p.Korisnik)
                .Where(p => p.RazgovorId == razgovorId)
                .OrderBy(p => p.DatumKreiranja)
                .ToListAsync();

            return poruke.Select(ToDto).ToList();
        }

        public async Task<PorukaDto> CreateAsync(int razgovorId, int korisnikId, bool isAdmin, PorukaCreateDto dto)
        {
            await _razgovorService.EnsureParticipantAsync(razgovorId, korisnikId, isAdmin);

            var razgovor = await _db.Razgovori.FirstOrDefaultAsync(r => r.RazgovorId == razgovorId)
                ?? throw new NotFoundException($"Razgovor {razgovorId} ne postoji.");

            if (razgovor.StatusRazgovora == StatusRazgovora.Zatvoren)
            {
                throw new ConflictException("Razgovor je zatvoren, nije moguće slati poruke.");
            }

            var poruka = new Poruka
            {
                RazgovorId = razgovorId,
                KorisnikId = korisnikId,
                Sadrzaj = dto.Sadrzaj,
                DatumKreiranja = DateTime.UtcNow
            };

            _db.Poruke.Add(poruka);
            await _db.SaveChangesAsync();

            await _db.Entry(poruka).Reference(p => p.Korisnik).LoadAsync();
            return ToDto(poruka);
        }

        private static PorukaDto ToDto(Poruka poruka) => new()
        {
            PorukaId = poruka.PorukaId,
            KorisnikId = poruka.KorisnikId,
            KorisnickoIme = poruka.Korisnik?.KorisnickoIme ?? string.Empty,
            RazgovorId = poruka.RazgovorId,
            DatumKreiranja = poruka.DatumKreiranja,
            Sadrzaj = poruka.Sadrzaj
        };
    }
}
