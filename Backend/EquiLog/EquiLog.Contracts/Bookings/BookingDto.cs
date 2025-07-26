namespace EquiLog.Contracts.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RidingArenaId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
