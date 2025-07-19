using EquiLog.Contracts.Users;
using EquiLog.Contracts.Common;

namespace EquiLog.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string userId);
        Task<ServiceResult> CreateUserAsync(CreateUserRequest request);
        Task<ServiceResult> UpdateUserAsync(string userId, CreateUserRequest request);
        Task<ServiceResult> DeleteUserAsync(string userId);
    }
}
