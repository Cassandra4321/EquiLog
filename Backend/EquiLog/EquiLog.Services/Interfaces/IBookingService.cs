using EquiLog.Contracts.Bookings;
using EquiLog.Contracts.Common;

namespace EquiLog.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllBookingsAsync();
        Task<List<BookingDto>> GetBookingsForArenaAsync(int ridingArenaId);
        Task<List<BookingDto>> GetBookingsForUserAsync(string userId);
        Task<(BookingDto? Booking, ServiceResult Result)> CreateBookingAsync(CreateBookingRequest request, string userId);
        Task<ServiceResult> DeleteBookingAsync(int bookingId, string userId);
        Task<(BookingDto? Booking, ServiceResult Result)> GetBookingByIdAsync(int bookingId, string userId);

    }
}
