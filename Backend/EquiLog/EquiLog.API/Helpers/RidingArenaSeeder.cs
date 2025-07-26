using EquiLog.DAL.Context;
using EquiLog.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EquiLog.API.Helpers
{
    public static class RidingArenaSeeder
    {
        public static async Task SeedingArenaAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!await context.RidingArena.AnyAsync())
            {
                var arenas = new List<RidingArena>
                {
                    new RidingArena
                    {
                        Name = "Ridhuset",
                        Description = "60x20 meter, fibersandunderlag. Det finns 9 hinderstöd och 29 bommar."
                    },
                    new RidingArena
                    {
                        Name = "Lilla ridbanan",
                        Description = "40x20 meter, sandunderlag. Det finns 3 hinderstöd och 10 bommar."
                    },
                    new RidingArena
                    {
                        Name = "Stora ridbanan",
                        Description = "80x40 meter, sandunderlag. Det finns 12 hinderstöd, 40 bommar och 8 sockerbitar."
                    }
                };

                context.RidingArena.AddRange(arenas);
                await context.SaveChangesAsync();
            }
        }
    }
}
