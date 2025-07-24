using EquiLog.Contracts.Horses;
using EquiLog.DAL.Models;

namespace EquiLog.Services.Mappers
{
    public static class HorseMapper
    {
        public static HorseDto ToDto(Horse horse)
        {
            return new HorseDto
            {
                Id = horse.Id,
                Name = horse.Name,
                Age = horse.Age,
                ImageUrl = horse.ImageUrl,
                OwnerId = horse.OwnerId,
                EmergencyContactNumber = horse.EmergencyContactNumber,
                CoRiderName = horse.CoRiderName,
                Gender = horse.Gender,
                HoofStatus = horse.HoofStatus,
                Pasture = horse.Pasture,
                Blanket = horse.Blanket,
                FlyMask = horse.FlyMask,
                Boots = horse.Boots,
                TurnoutInstructions = horse.TurnoutInstructions,
                IntakeInstructions = horse.IntakeInstructions,
                FeedingInstructions = horse.FeedingInstructions,
                OtherInfo = horse.OtherInfo
            };
        }

        public static Horse ToHorse(CreateHorseRequest request)
        {
            return new Horse
            {
                Name = request.Name,
                Age = request.Age,
                ImageUrl = request.ImageUrl,
                OwnerId = request.OwnerId,
                EmergencyContactNumber = request.EmergencyContactNumber,
                CoRiderName = request.CoRiderName,
                Gender = request.Gender,
                HoofStatus = request.HoofStatus,
                Pasture = request.Pasture,
                Blanket = request.Blanket,
                FlyMask = request.FlyMask,
                Boots = request.Boots,
                TurnoutInstructions = request.TurnoutInstructions,
                IntakeInstructions = request.IntakeInstructions,
                FeedingInstructions = request.FeedingInstructions,
                OtherInfo = request.OtherInfo
            };
        }

        public static void UpdateHorseFromRequest(Horse horse, UpdateHorseRequest request)
        {
            horse.Name = request.Name;
            horse.Age = request.Age;
            horse.ImageUrl = request.ImageUrl;
            horse.EmergencyContactNumber = request.EmergencyContactNumber;
            horse.CoRiderName = request.CoRiderName;
            horse.Gender = request.Gender;
            horse.HoofStatus = request.HoofStatus;
            horse.Pasture = request.Pasture;
            horse.Blanket = request.Blanket;
            horse.FlyMask = request.FlyMask;
            horse.Boots = request.Boots;
            horse.TurnoutInstructions = request.TurnoutInstructions;
            horse.IntakeInstructions = request.IntakeInstructions;
            horse.FeedingInstructions = request.FeedingInstructions;
            horse.OtherInfo = request.OtherInfo;
        }
    }
}
