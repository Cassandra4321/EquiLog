namespace EquiLog.Contracts.Horses
{
    public class HorseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public int Age { get; set; }
        public string ImageUrl { get; set; } = string.Empty!;
        public string OwnerId { get; set; } = string.Empty!;
        public string OwnerName { get; set; } = string.Empty!;
        public string EmergencyContactNumber { get; set; } = string.Empty!;
        public string? CoRiderName { get; set; }
        public string Gender { get; set; } = string.Empty!;
        public string HoofStatus { get; set; } = string.Empty!;
        public string Pasture { get; set; } = string.Empty!;
        public string? Blanket { get; set; }
        public string? FlyMask { get; set; }
        public string? Boots { get; set; }
        public string? TurnoutInstructions { get; set; }
        public string? IntakeInstructions { get; set; }
        public string? FeedingInstructions { get; set; }
        public string? OtherInfo { get; set; }
    }
}
