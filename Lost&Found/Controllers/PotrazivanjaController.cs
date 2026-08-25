using Lost_Found.DTOs.Potrazivanje;
using Lost_Found.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lost_Found.Controllers
{
    [ApiController]
    [Authorize]
    public class PotrazivanjaController : ApiControllerBase
    {
        private readonly IPotrazivanjeService _potrazivanjeService;

        public PotrazivanjaController(IPotrazivanjeService potrazivanjeService)
        {
            _potrazivanjeService = potrazivanjeService;
        }

        [HttpPost("api/oglasi/{oglasId:int}/potrazivanja")]
        [Authorize(Roles = "StandardniKorisnik")]
        public async Task<ActionResult<PotrazivanjeDto>> Create(int oglasId)
        {
            var result = await _potrazivanjeService.CreateAsync(oglasId, CurrentKorisnikId);
            return CreatedAtAction(nameof(GetForOglas), new { oglasId }, result);
        }

        [HttpGet("api/oglasi/{oglasId:int}/potrazivanja")]
        public async Task<ActionResult<IReadOnlyList<PotrazivanjeDto>>> GetForOglas(int oglasId)
        {
            return Ok(await _potrazivanjeService.GetForOglasAsync(oglasId, CurrentKorisnikId, IsAdmin));
        }

        [HttpGet("api/potrazivanja/mine")]
        public async Task<ActionResult<IReadOnlyList<PotrazivanjeDto>>> GetMine()
        {
            return Ok(await _potrazivanjeService.GetMineAsync(CurrentKorisnikId));
        }

        [HttpPatch("api/oglasi/{oglasId:int}/potrazivanja/{korisnikId:int}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PotrazivanjeDto>> UpdateStatus(int oglasId, int korisnikId, AzurirajStatusDto dto)
        {
            return Ok(await _potrazivanjeService.UpdateStatusAsync(oglasId, korisnikId, dto.Status));
        }

        [HttpDelete("api/oglasi/{oglasId:int}/potrazivanja/{korisnikId:int}")]
        public async Task<IActionResult> Withdraw(int oglasId, int korisnikId)
        {
            await _potrazivanjeService.WithdrawAsync(oglasId, korisnikId, CurrentKorisnikId);
            return NoContent();
        }
    }
}
