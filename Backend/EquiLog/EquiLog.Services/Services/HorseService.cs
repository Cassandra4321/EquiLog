using EquiLog.Contracts.Common;
using EquiLog.Contracts.Horses;
using EquiLog.DAL.Context;
using EquiLog.Services.Interfaces;
using EquiLog.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace EquiLog.Services.Services
{
    public class HorseService : IHorseService
    {
        private readonly AppDbContext _context;

        public HorseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HorseDto>> GetAllHorsesAsync()
        {
            var horses = await _context.Horses.ToListAsync();
            return horses.Select(HorseMapper.ToDto).ToList();   
        }

        public async Task<HorseDto?> GetHorseByIdAsync(int id)
        {
            var horse = await _context.Horses.FindAsync(id);
            if (horse == null)
            {
                return null;
            }

            return HorseMapper.ToDto(horse);
        }

        public async Task<(HorseDto? Horse, ServiceResult Result)> CreateHorseAsync(CreateHorseRequest request)
        {
            try
            {
                var horse = HorseMapper.ToHorse(request);
                _context.Horses.Add(horse);
                await _context.SaveChangesAsync();

                return (Horse: HorseMapper.ToDto(horse), Result: ServiceResult.Ok());
            }
            catch (Exception ex)
            {
                return (Horse: null, Result: ServiceResult.Fail($"Could not create the horse: {ex.Message}"));
            }
        }

        public async Task<ServiceResult> UpdateHorseAsync(int id, UpdateHorseRequest request)
        {
            var horse = await _context.Horses.FindAsync(id);
            if (horse == null)
            {
                return ServiceResult.Fail("Horse could not be found.");
            }

            try
            {
                HorseMapper.UpdateHorseFromRequest(horse, request);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"Could not update the horse: {ex.Message}");
            }
        }

        public async Task<ServiceResult> DeleteHorseAsync(int id)
        {
            var horse = await _context.Horses.FindAsync(id);
            if (horse == null)
            {
                return ServiceResult.Fail("Horse could not be found.");
            }

            try
            {
                _context.Horses.Remove(horse);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch(Exception ex)
            {
                return ServiceResult.Fail($"Could not remove the horse: {ex.Message}");
            }
        }
    }
}
