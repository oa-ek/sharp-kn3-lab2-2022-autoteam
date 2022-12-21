using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class VehicleBrand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleBrandId { get; set; }
        public string? VehicleBrandName { get; set; }

        public virtual ICollection<VehicleModel>? VehicleModels { get; set; }
    }
}