using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EquiLog.DAL.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

    }
}
