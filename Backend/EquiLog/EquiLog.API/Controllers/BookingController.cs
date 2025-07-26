using EquiLog.Contracts.Bookings;
using EquiLog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EquiLog.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("bookings")]
        [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{bookingId}")]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var (booking, result) = await _bookingService.GetBookingByIdAsync(bookingId, userId);
            if (!result.Success || booking == null)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(booking);
        }

        [HttpGet("arena/{arenaId}")]
        [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BookingDto>>> GetBookingsForArena(int arenaId)
        {
            var bookings = await _bookingService.GetBookingsForArenaAsync(arenaId);
            return Ok(bookings);
        }

        [HttpGet("userbookings")]
        [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookingDto>>> GetBookingsForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
               
            var bookings = await _bookingService.GetBookingsForUserAsync(userId);
            return Ok(bookings);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
               
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var (booking, result) = await _bookingService.CreateBookingAsync(request, userId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
                
            return CreatedAtAction(nameof(GetBookingsForUser), new { }, booking);
        }

        [HttpDelete("{bookingId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _bookingService.DeleteBookingAsync(bookingId, userId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
