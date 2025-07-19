using EquiLog.Contracts.Users;
using EquiLog.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace EquiLog.Services.Mappers
{
    public static class UserMapper
    {
        public static async Task<UserDto> ToDto(AppUser user, UserManager<AppUser> userManager)
        {
            var roles = await userManager.GetRolesAsync(user);
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList()
            };
        }
    }
}
