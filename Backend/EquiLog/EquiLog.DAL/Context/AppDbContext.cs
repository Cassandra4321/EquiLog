using EquiLog.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquiLog.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Horse> Horses { get; set; } = null!;

        public DbSet<RidingArena> RidingArena { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
