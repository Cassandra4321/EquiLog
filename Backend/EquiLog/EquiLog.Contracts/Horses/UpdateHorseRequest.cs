using System.ComponentModel.DataAnnotations;

namespace EquiLog.Contracts.Horses
{
    public class UpdateHorseRequest
    {

        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty!;

        [Required]
        [StringLength(15)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Invalid Swedish phone number.")]
        public string EmergencyContactNumber { get; set; } = string.Empty!;

        public string? CoRiderName { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty!;

        [Required]
        public string HoofStatus { get; set; } = string.Empty!;

        [Required]
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
