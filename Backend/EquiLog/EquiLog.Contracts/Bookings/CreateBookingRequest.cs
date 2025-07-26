namespace EquiLog.Contracts.Bookings
{
    public class CreateBookingRequest
    {
        public int RidingArenaId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
