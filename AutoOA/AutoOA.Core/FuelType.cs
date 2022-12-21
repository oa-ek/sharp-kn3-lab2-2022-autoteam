using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class FuelType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FuelTypeId { get; set; }
        public string? FuelName { get; set; }
        public string? IconPath { get; set; } = @"\Images\fuelTypeIcon.png";

        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}