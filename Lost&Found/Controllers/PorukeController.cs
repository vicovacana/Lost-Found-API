using Lost_Found.DTOs.Poruka;
using Lost_Found.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lost_Found.Controllers
{
    [ApiController]
    [Authorize]
    public class PorukeController : ApiControllerBase
    {
        private readonly IPorukaService _porukaService;

        public PorukeController(IPorukaService porukaService)
        {
            _porukaService = porukaService;
        }

        [HttpGet("api/razgovori/{razgovorId:int}/poruke")]
        public async Task<ActionResult<IReadOnlyList<PorukaDto>>> GetForRazgovor(int razgovorId)
        {
            return Ok(await _porukaService.GetForRazgovorAsync(razgovorId, CurrentKorisnikId, IsAdmin));
        }

        [HttpPost("api/razgovori/{razgovorId:int}/poruke")]
        public async Task<ActionResult<PorukaDto>> Create(int razgovorId, PorukaCreateDto dto)
        {
            var result = await _porukaService.CreateAsync(razgovorId, CurrentKorisnikId, IsAdmin, dto);
            return CreatedAtAction(nameof(GetForRazgovor), new { razgovorId }, result);
        }
    }
}
