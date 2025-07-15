using EquiLog.Contracts.Auth;

namespace EquiLog.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
