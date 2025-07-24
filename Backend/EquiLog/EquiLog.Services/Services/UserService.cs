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
                var error = string.Join("; ", result.Errors.Select(e => e.Description));
                return ServiceResult.Fail(error);
            }

            var roleResult =  await _userManager.AddToRoleAsync(user, request.Role);
            if (!roleResult.Succeeded)
            {
                var error = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                return ServiceResult.Fail($"User created, but failed to assign role: {error}");
            }

            return ServiceResult.Ok();
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
                var error = string.Join("; ", result.Errors.Select(e => e.Description));
                return ServiceResult.Fail(error);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(request.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, request.Role);
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
            if (!result.Succeeded)
            {
                var error = string.Join("; ", result.Errors.Select(e => e.Description));
                return ServiceResult.Fail(error);
            }

            return ServiceResult.Ok();
        }
    }
}
