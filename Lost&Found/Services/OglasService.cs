using Lost_Found.Common;
using Lost_Found.Data;
using Lost_Found.DTOs.Oglas;
using Lost_Found.Models;
using Lost_Found.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Lost_Found.Services
{
    public class OglasService : IOglasService
    {
        private readonly ApplicationDbContext _db;

        public OglasService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<OglasDto>> GetAllAsync(TipOglasa? tip, int? kreatorId, int? adminId)
        {
            var query = _db.Oglasi.Include(o => o.Kreator).AsQueryable();

            if (tip.HasValue) query = query.Where(o => o.Tip == tip.Value);
            if (kreatorId.HasValue) query = query.Where(o => o.KreatorId == kreatorId.Value);
            if (adminId.HasValue) query = query.Where(o => o.AdminId == adminId.Value);

            var oglasi = await query.OrderByDescending(o => o.DatumKreiranja).ToListAsync();
            return oglasi.Select(ToDto).ToList();
        }

        public async Task<OglasDto> GetByIdAsync(int oglasId)
        {
            var oglas = await _db.Oglasi.Include(o => o.Kreator)
                .FirstOrDefaultAsync(o => o.OglasId == oglasId)
                ?? throw new NotFoundException($"Oglas {oglasId} ne postoji.");

            return ToDto(oglas);
        }

        public async Task<OglasDto> CreateAsync(int kreatorId, OglasCreateDto dto)
        {
            var oglas = new Oglas
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                Tip = dto.Tip,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Fotografija = dto.Fotografija,
                KreatorId = kreatorId,
                DatumKreiranja = DateTime.UtcNow
            };

            _db.Oglasi.Add(oglas);
            await _db.SaveChangesAsync();

            return await GetByIdAsync(oglas.OglasId);
        }

        public async Task<OglasDto> UpdateAsync(int oglasId, int currentKorisnikId, bool isAdmin, OglasUpdateDto dto)
        {
            var oglas = await _db.Oglasi.Include(o => o.Kreator)
                .FirstOrDefaultAsync(o => o.OglasId == oglasId)
                ?? throw new NotFoundException($"Oglas {oglasId} ne postoji.");

            if (!isAdmin && oglas.KreatorId != currentKorisnikId)
            {
                throw new ForbiddenException("Samo vlasnik oglasa ili admin mogu da ga izmene.");
            }

            oglas.Naziv = dto.Naziv;
            oglas.Opis = dto.Opis;
            oglas.Tip = dto.Tip;
            oglas.Latitude = dto.Latitude;
            oglas.Longitude = dto.Longitude;
            oglas.Fotografija = dto.Fotografija;

            await _db.SaveChangesAsync();

            return ToDto(oglas);
        }

        public async Task DeleteAsync(int oglasId, int currentKorisnikId, bool isAdmin)
        {
            var oglas = await _db.Oglasi.FirstOrDefaultAsync(o => o.OglasId == oglasId)
                ?? throw new NotFoundException($"Oglas {oglasId} ne postoji.");

            if (!isAdmin && oglas.KreatorId != currentKorisnikId)
            {
                throw new ForbiddenException("Samo vlasnik oglasa ili admin mogu da ga obrišu.");
            }

            _db.Oglasi.Remove(oglas);
            await _db.SaveChangesAsync();
        }

        public async Task<OglasDto> AssignAdminAsync(int oglasId, int? adminId)
        {
            var oglas = await _db.Oglasi.Include(o => o.Kreator)
                .FirstOrDefaultAsync(o => o.OglasId == oglasId)
                ?? throw new NotFoundException($"Oglas {oglasId} ne postoji.");

            if (adminId.HasValue)
            {
                var adminPostoji = await _db.Admini.AnyAsync(a => a.KorisnikId == adminId.Value);
                if (!adminPostoji)
                {
                    throw new NotFoundException($"Admin {adminId} ne postoji.");
                }
            }

            oglas.AdminId = adminId;
            await _db.SaveChangesAsync();

            return ToDto(oglas);
        }

        private static OglasDto ToDto(Oglas oglas) => new()
        {
            OglasId = oglas.OglasId,
            Naziv = oglas.Naziv,
            Opis = oglas.Opis,
            DatumKreiranja = oglas.DatumKreiranja,
            Tip = oglas.Tip,
            Latitude = oglas.Latitude,
            Longitude = oglas.Longitude,
            Fotografija = oglas.Fotografija,
            KreatorId = oglas.KreatorId,
            KreatorKorisnickoIme = oglas.Kreator?.KorisnickoIme ?? string.Empty,
            AdminId = oglas.AdminId
        };
    }
}
