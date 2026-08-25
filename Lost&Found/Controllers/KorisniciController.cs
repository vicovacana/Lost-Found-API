using Lost_Found.DTOs.Korisnik;
using Lost_Found.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lost_Found.Controllers
{
    [ApiController]
    [Route("api/korisnici")]
    [Authorize]
    public class KorisniciController : ApiControllerBase
    {
        private readonly IKorisnikService _korisnikService;

        public KorisniciController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<KorisnikDto>>> GetAll()
        {
            return Ok(await _korisnikService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<KorisnikDto>> GetById(int id)
        {
            return Ok(await _korisnikService.GetByIdAsync(id));
        }

        [HttpGet("me")]
        public async Task<ActionResult<KorisnikDto>> GetMe()
        {
            return Ok(await _korisnikService.GetByIdAsync(CurrentKorisnikId));
        }

        [HttpPut("me")]
        public async Task<ActionResult<KorisnikDto>> UpdateMe(KorisnikUpdateDto dto)
        {
            return Ok(await _korisnikService.UpdateAsync(CurrentKorisnikId, dto));
        }

        [HttpPost("admins")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<KorisnikDto>> CreateAdmin(KreirajAdminaDto dto)
        {
            var result = await _korisnikService.CreateAdminAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.KorisnikId }, result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _korisnikService.DeleteAsync(id);
            return NoContent();
        }
    }
}
