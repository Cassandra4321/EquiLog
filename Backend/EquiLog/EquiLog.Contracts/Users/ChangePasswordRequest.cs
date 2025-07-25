using System.ComponentModel.DataAnnotations;

namespace EquiLog.Contracts.Users
{
    public class ChangePasswordRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
