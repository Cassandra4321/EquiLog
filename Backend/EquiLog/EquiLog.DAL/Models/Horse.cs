﻿using System.ComponentModel.DataAnnotations;

namespace EquiLog.DAL.Models
{
    public class Horse
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty!;

        [Required]
        public string OwnerId { get; set; } = string.Empty!;

        public AppUser Owner { get; set; } = null!;

        [Required]
        [StringLength(15)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Invalid Swedish phone number.")]
        public string EmergencyContactNumber { get; set; } = string.Empty!;

        public string? CoRiderName { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty!;

        [Required]
        public string HoofStatus { get; set; } = string.Empty!; // for example "Shoes"/"Barefoot"

        [Required]
        public string Pasture { get; set; } = string.Empty!; // for example "Pasture number 3" / "Free stall"

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
