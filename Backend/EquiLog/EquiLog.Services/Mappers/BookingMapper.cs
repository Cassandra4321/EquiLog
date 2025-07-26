using EquiLog.Contracts.Bookings;
using EquiLog.DAL.Models;

namespace EquiLog.Services.Mappers
{
    public static class BookingMapper
    {
        public static BookingDto ToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                RidingArenaId = booking.RidingArenaId,
                UserId = booking.UserId
            };
        }

        public static Booking ToBooking(CreateBookingRequest request, string userId)
        {
            return new Booking
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                RidingArenaId = request.RidingArenaId,
                UserId = userId
            };
        }
    }
}
