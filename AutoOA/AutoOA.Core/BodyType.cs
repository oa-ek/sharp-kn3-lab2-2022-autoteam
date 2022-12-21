using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class BodyType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BodyTypeId { get; set; }
        public string? BodyTypeName { get; set; }
        public string? IconPath { get; set; } = @"\Images\BodyTypeIcon.png"; //TO DO PRAVKI

        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}