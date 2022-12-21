using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class DriveType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriveTypeId { get; set; }
        public string? DriveTypeName { get; set; }

        public virtual ICollection<Vehicle>? Vehicle { get; set; }
    }
}
