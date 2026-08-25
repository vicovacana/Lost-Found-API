using Lost_Found.DTOs.Oglas;
using Lost_Found.Models.Enums;
using Lost_Found.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lost_Found.Controllers
{
    [ApiController]
    [Route("api/oglasi")]
    public class OglasiController : ApiControllerBase
    {
        private readonly IOglasService _oglasService;

        public OglasiController(IOglasService oglasService)
        {
            _oglasService = oglasService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<OglasDto>>> GetAll(
            [FromQuery] TipOglasa? tip, [FromQuery] int? kreatorId, [FromQuery] int? adminId)
        {
            return Ok(await _oglasService.GetAllAsync(tip, kreatorId, adminId));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<OglasDto>> GetById(int id)
        {
            return Ok(await _oglasService.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "StandardniKorisnik")]
        public async Task<ActionResult<OglasDto>> Create(OglasCreateDto dto)
        {
            var result = await _oglasService.CreateAsync(CurrentKorisnikId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.OglasId }, result);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<OglasDto>> Update(int id, OglasUpdateDto dto)
        {
            return Ok(await _oglasService.UpdateAsync(id, CurrentKorisnikId, IsAdmin, dto));
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _oglasService.DeleteAsync(id, CurrentKorisnikId, IsAdmin);
            return NoContent();
        }

        [HttpPatch("{id:int}/admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OglasDto>> AssignAdmin(int id, DodeliAdminaDto dto)
        {
            var adminId = dto.AdminId ?? CurrentKorisnikId;
            return Ok(await _oglasService.AssignAdminAsync(id, adminId));
        }

        [HttpDelete("{id:int}/admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OglasDto>> ClearAdmin(int id)
        {
            return Ok(await _oglasService.AssignAdminAsync(id, null));
        }
    }
}
