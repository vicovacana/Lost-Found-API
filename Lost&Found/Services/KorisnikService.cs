using Lost_Found.Common;
using Lost_Found.Data;
using Lost_Found.DTOs.Korisnik;
using Lost_Found.Models;
using Microsoft.EntityFrameworkCore;

namespace Lost_Found.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly ApplicationDbContext _db;

        public KorisnikService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<KorisnikDto>> GetAllAsync()
        {
            var korisnici = await _db.Korisnici.OrderBy(k => k.KorisnikId).ToListAsync();
            return korisnici.Select(ToDto).ToList();
        }

        public async Task<KorisnikDto> GetByIdAsync(int korisnikId)
        {
            var korisnik = await _db.Korisnici.FindAsync(korisnikId)
                ?? throw new NotFoundException($"Korisnik {korisnikId} ne postoji.");

            return ToDto(korisnik);
        }

        public async Task<KorisnikDto> UpdateAsync(int korisnikId, KorisnikUpdateDto dto)
        {
            var korisnik = await _db.Korisnici.FindAsync(korisnikId)
                ?? throw new NotFoundException($"Korisnik {korisnikId} ne postoji.");

            var zauzeto = await _db.Korisnici.AnyAsync(k =>
                k.KorisnikId != korisnikId &&
                (k.KorisnickoIme == dto.KorisnickoIme || k.Email == dto.Email));
            if (zauzeto)
            {
                throw new ConflictException("Korisničko ime ili email su već zauzeti.");
            }

            korisnik.KorisnickoIme = dto.KorisnickoIme;
            korisnik.Email = dto.Email;
            await _db.SaveChangesAsync();

            return ToDto(korisnik);
        }

        public async Task<KorisnikDto> CreateAdminAsync(KreirajAdminaDto dto)
        {
            var postoji = await _db.Korisnici.AnyAsync(k =>
                k.KorisnickoIme == dto.KorisnickoIme || k.Email == dto.Email);
            if (postoji)
            {
                throw new ConflictException("Korisničko ime ili email su već zauzeti.");
            }

            var admin = new Admin
            {
                KorisnickoIme = dto.KorisnickoIme,
                Email = dto.Email,
                LozinkaHash = BCrypt.Net.BCrypt.HashPassword(dto.Lozinka),
                VremeKreiranja = DateTime.UtcNow
            };

            _db.Admini.Add(admin);
            await _db.SaveChangesAsync();

            return ToDto(admin);
        }

        public async Task DeleteAsync(int korisnikId)
        {
            var korisnik = await _db.Korisnici.FindAsync(korisnikId)
                ?? throw new NotFoundException($"Korisnik {korisnikId} ne postoji.");

            _db.Korisnici.Remove(korisnik);
            await _db.SaveChangesAsync();
        }

        private static KorisnikDto ToDto(Korisnik korisnik) => new()
        {
            KorisnikId = korisnik.KorisnikId,
            KorisnickoIme = korisnik.KorisnickoIme,
            Email = korisnik.Email,
            VremeKreiranja = korisnik.VremeKreiranja,
            Uloga = korisnik is Admin ? "Admin" : "StandardniKorisnik"
        };
    }
}
