using EquiLog.Contracts.Common;
using EquiLog.Contracts.Users;
using EquiLog.DAL.Models;
using EquiLog.Services.Interfaces;
using EquiLog.Services.Mappers;
using Microsoft.AspNetCore.Identity;

namespace EquiLog.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(await UserMapper.ToDto(user, _userManager));
            }

            return userDtos;
        }

        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return await UserMapper.ToDto(user, _userManager);
        }

        public async Task<ServiceResult> CreateUserAsync(CreateUserRequest request)
        {
            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return result.ToServiceResult();
            }

            var roleResult =  await _userManager.AddToRoleAsync(user, request.Role);

            return roleResult.ToServiceResult("User craeted, but failed to assign role.");
        }

        public async Task<ServiceResult> UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            if (userId != request.Id)
            {
                return ServiceResult.Fail("User ID missmatch.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.Fail("User not found.");
            }

            user.UserName = request.Email;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result.ToServiceResult();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(request.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
                return roleResult.ToServiceResult("User updated, but role change failed");
            }

            return ServiceResult.Ok();
        }
        public async Task<ServiceResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.Fail("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            return result.ToServiceResult();
        }

        public async Task<ServiceResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.Fail("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.ToServiceResult("Password change failed.");
        }

        public async Task<ServiceResult> ResetPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if ( user == null )
            {
                return ServiceResult.Fail("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.ToServiceResult("Password reset failed");
        }
    }
}
