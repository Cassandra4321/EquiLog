using EquiLog.Contracts.Bookings;
using EquiLog.DAL.Models;

namespace EquiLog.Services.Mappers
{
    public static class RidingArenaMapper
    {
        public static RidingArenaDto ToDto(RidingArena arena)
        {
            return new RidingArenaDto
            {
                Id = arena.Id,
                Name = arena.Name,
                Description = arena.Description
            };
        }
    }
}
