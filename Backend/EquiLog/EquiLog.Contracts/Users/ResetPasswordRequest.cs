using System.ComponentModel.DataAnnotations;

namespace EquiLog.Contracts.Users
{
    public class ResetPasswordRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
