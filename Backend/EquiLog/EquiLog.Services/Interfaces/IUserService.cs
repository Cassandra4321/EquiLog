using EquiLog.Contracts.Users;
using EquiLog.Contracts.Common;

namespace EquiLog.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string userId);
        Task<ServiceResult> CreateUserAsync(CreateUserRequest request);
        Task<ServiceResult> UpdateUserAsync(string userId, UpdateUserRequest request);
        Task<ServiceResult> DeleteUserAsync(string userId);
        Task<ServiceResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<ServiceResult> ResetPasswordAsync(string userId, string newPassword);
    }
}
