using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class VehicleColor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ColorName { get; set; }

        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
