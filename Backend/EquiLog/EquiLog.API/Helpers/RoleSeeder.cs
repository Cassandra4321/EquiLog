using EquiLog.Contracts.Auth;
using Microsoft.AspNetCore.Identity;

namespace EquiLog.API.Helpers
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { Roles.StableOwner, Roles.Staff, Roles.HorseOwner, Roles.Rider };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
