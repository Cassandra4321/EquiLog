using EquiLog.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace EquiLog.API.Helpers
{
    public static class UserSeeder
    {
        public static async Task SeedSuperAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var superAdminEmail = configuration["SuperAdmin:Email"];
            var superAdminPassword = configuration["SuperAdmin:Password"];

            if (string.IsNullOrWhiteSpace(superAdminEmail) || string.IsNullOrWhiteSpace(superAdminPassword))
            {
                throw new Exception("SuperAdmin credentials not configured properly.");
            }

            var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

            if (superAdmin == null)
            {
                var newUser = new AppUser
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    FirstName = "Stable",
                    LastName = "Owner",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, superAdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "StableOwner");
                }
                else
                {
                    throw new Exception("Could not create SuperAdmin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
