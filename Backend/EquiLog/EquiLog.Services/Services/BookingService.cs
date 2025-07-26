using EquiLog.Contracts.Bookings;
using EquiLog.Contracts.Common;
using EquiLog.DAL.Context;
using EquiLog.Services.Interfaces;
using EquiLog.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace EquiLog.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return bookings.Select(BookingMapper.ToDto).ToList();
        }

        public async Task<(BookingDto? Booking, ServiceResult Result)> GetBookingByIdAsync(int bookingId, string userId)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);

            if (booking == null)
            {
                return (null, ServiceResult.Fail("Booking not found or access denied"));
            }
            return (BookingMapper.ToDto(booking), ServiceResult.Ok());
        }

        public async Task<List<BookingDto>> GetBookingsForArenaAsync(int arenaId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.RidingArenaId == arenaId)
                .ToListAsync();

            return bookings.Select(BookingMapper.ToDto).ToList();
        }

        public async Task<List<BookingDto>> GetBookingsForUserAsync(string userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bookings.Select(BookingMapper.ToDto).ToList();
        }

        public async Task<(BookingDto? Booking, ServiceResult Result)> CreateBookingAsync(CreateBookingRequest request, string userId)
        {
            var arena = await _context.RidingArena.FindAsync(request.RidingArenaId);
            if (arena == null)
            {
                return (null, ServiceResult.Fail("Riding arena not found"));
            }

            var booking = BookingMapper.ToBooking(request, userId);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return (BookingMapper.ToDto(booking), ServiceResult.Ok());
        }

        public async Task<ServiceResult> DeleteBookingAsync(int bookingId, string userId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return ServiceResult.Fail("Booking not found");
            }

            if (booking.UserId != userId)
            {
                return ServiceResult.Fail("Unauthorized to delete this booking");
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
