using EquiLog.Contracts.Auth;
using EquiLog.Contracts.Horses;
using EquiLog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EquiLog.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HorseController : ControllerBase
    {
        private readonly IHorseService _horseService;

        public HorseController(IHorseService horseService)
        {
            _horseService = horseService;
        }

        [HttpGet("horses")]
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
                return NotFound("Horse not found.");
            }
            return Ok(horse);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(HorseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateHorse([FromBody] CreateHorseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isStableOwnerOrStaff = User.IsInRole("StableOwner") || User.IsInRole("StableStaff");

            if (!isStableOwnerOrStaff)
            {
                request.OwnerId = currentUserId!;
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

            var existingHorse = await _horseService.GetHorseByIdAsync(id);
            if (existingHorse == null)
            {
                return NotFound("Horse not found.");
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isStableOwnerOrStaff = User.IsInRole(Roles.StableOwner) || User.IsInRole(Roles.Staff);

            if (!isStableOwnerOrStaff && existingHorse.OwnerId != currentUserId)
            {
                return Forbid("You can only update your own horses.");
            }

            var result = await _horseService.UpdateHorseAsync(id, request);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHorse(int id)
        {
            var existingHorse = await _horseService.GetHorseByIdAsync(id);
            if (existingHorse == null)
            {
                return NotFound("Horse not found.");
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isStableOwnerOrStaff = User.IsInRole(Roles.StableOwner) || User.IsInRole(Roles.Staff);

            if (!isStableOwnerOrStaff && existingHorse.OwnerId != currentUserId)
            {
                return Forbid("You can only delete your own horses.");
            }

            var result = await _horseService.DeleteHorseAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }
    }
}
