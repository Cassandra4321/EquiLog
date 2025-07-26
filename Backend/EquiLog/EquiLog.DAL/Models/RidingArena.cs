using System.ComponentModel.DataAnnotations;

namespace EquiLog.DAL.Models
{
    public class RidingArena
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
