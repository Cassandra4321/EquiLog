using EquiLog.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace EquiLog.DAL.Models
{
    public class Horse
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!; // ForeignKey to AppUser

        public AppUser Owner { get; set; } = null!;

        [Required]
        public string EmergencyContactNumber { get; set; } = null!;

        public string? CoRiderName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string HoofStatus { get; set; } = null!; // for example "Shoes"/"Barefoot"

        [Required]
        public string Pasture { get; set; } = null!; // for example "Pasture number 3" / "Free stall"

        // Turnout gear
        public string? Blanket { get; set; }
        public string? FlyMask { get; set; }
        public string? Boots { get; set; }

        public string? TurnoutInstructions { get; set; }
        public string? IntakeInstructions { get; set; }
        public string? FeedingInstructions { get; set; }

        public string? OtherInfo { get; set; }
    }
}
