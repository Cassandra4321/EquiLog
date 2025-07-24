using EquiLog.Contracts.Auth;
using System.ComponentModel.DataAnnotations;

namespace EquiLog.Contracts.Users
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = Roles.Staff;
    }
}
