using EquiLog.DAL.Models;
using EquiLog.Contracts.Auth;
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
            var superAdminPhoneNumber = configuration["SuperAdmin:PhoneNumber"];

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
                    PhoneNumber = superAdminPhoneNumber,
                    PhoneNumberConfirmed = true,
                    FirstName = "Stable",
                    LastName = "Owner",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, superAdminPassword);

                if (result.Succeeded)
                {
                    var createdUser = await userManager.FindByEmailAsync(superAdminEmail);
                    if (createdUser == null)
                    {
                        throw new Exception("Could not find created SuperAdmin.");
                    }

                    var phoneResult = await userManager.SetPhoneNumberAsync(createdUser, superAdminPhoneNumber);

                    if (!phoneResult.Succeeded)
                    {
                        throw new Exception("Could not set phone number: " + string.Join(", ", phoneResult.Errors.Select(e => e.Description)));
                    }

                    await userManager.AddToRoleAsync(newUser, Roles.StableOwner);
                }
                else
                {
                    throw new Exception("Could not create SuperAdmin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        public static async Task SeedTestUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var horseOwnerEmail = "horseowner@test.com";
            var horseOwnerPassword = "Test123!";

            var existingHorseOwner = await userManager.FindByEmailAsync(horseOwnerEmail);
            if (existingHorseOwner == null)
            {
                var horseOwnerUser = new AppUser
                {
                    UserName = horseOwnerEmail,
                    Email = horseOwnerEmail,
                    FirstName = "Test",
                    LastName = "HorseOwner",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(horseOwnerUser, horseOwnerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(horseOwnerUser, Roles.HorseOwner);
                }
            }

            var riderEmail = "rider@test.com";
            var riderPassword = "Test123!";

            var existingRider = await userManager.FindByEmailAsync(riderEmail);
            if (existingRider == null)
            {
                var riderUser = new AppUser
                {
                    UserName = riderEmail,
                    Email = riderEmail,
                    FirstName = "Test",
                    LastName = "Rider",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(riderUser, riderPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(riderUser, Roles.Rider);
                }
            }

            var staffEmail = "staff@test.com";
            var staffPassword = "Test123!";

            var existingStaff = await userManager.FindByEmailAsync(staffEmail);
            if (existingStaff == null)
            {
                var staffUser = new AppUser
                {
                    UserName = staffEmail,
                    Email = staffEmail,
                    FirstName = "Test",
                    LastName = "Staff",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(staffUser, staffPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(staffUser, Roles.Staff);
                }
            }
        }
    }
}
