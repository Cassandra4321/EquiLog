namespace EquiLog.DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;

        public int RidingArenaId { get; set; }
        public RidingArena RidingArena { get; set; } = null!;
    }
}
