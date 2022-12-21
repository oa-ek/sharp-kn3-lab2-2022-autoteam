using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class GearBox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearBoxId { get; set; }
        public string? GearBoxName { get; set; }
        public string? IconPath { get; set; } = @"\Images\gearBoxIcon.png";

        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}