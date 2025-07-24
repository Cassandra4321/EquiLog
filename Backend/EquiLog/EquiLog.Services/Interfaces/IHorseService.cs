using EquiLog.Contracts.Common;
using EquiLog.Contracts.Horses;

namespace EquiLog.Services.Interfaces
{
    public interface IHorseService
    {
        Task<List<HorseDto>> GetAllHorsesAsync();
        Task<HorseDto?> GetHorseByIdAsync(int id);
        Task<(HorseDto? Horse, ServiceResult Result)> CreateHorseAsync(CreateHorseRequest request);
        Task<ServiceResult> UpdateHorseAsync(int id, UpdateHorseRequest request);
        Task<ServiceResult> DeleteHorseAsync(int id);
    }
}
