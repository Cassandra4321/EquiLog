using EquiLog.Contracts.Horses;
using EquiLog.Contracts.Users;
using EquiLog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EquiLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorseController : ControllerBase
    {
        private readonly IHorseService _horseService;

        public HorseController(IHorseService horseService)
        {
            _horseService = horseService;
        }

        [HttpGet]
        [Route("/api/Horses")]
        [ProducesResponseType(typeof(List<HorseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<HorseDto>>> GetAllHorses()
        {
            return Ok(await _horseService.GetAllHorsesAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HorseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HorseDto>> GetHorseById(int id)
        {
            var horse = await _horseService.GetHorseByIdAsync(id);
            if (horse == null)
            {
                return NotFound();
            }
            return Ok(horse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(HorseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateHorse([FromBody] CreateHorseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (horse, result) = await _horseService.CreateHorseAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(horse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHorse(int id, [FromBody] UpdateHorseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _horseService.UpdateHorseAsync(id, request);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return NoContent();
        }

        [HttpDelete ("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHorse(int id)
        {
            var result = await _horseService.DeleteHorseAsync(id);

            if(!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return NoContent();
        }
    }
}
