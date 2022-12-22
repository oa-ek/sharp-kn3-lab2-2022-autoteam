using AutoOA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOA.Repository.Dto.ColorDto
{
    public class VehicleColorReadDto
    {
        public int Id { get; set; }
        public string? ColorName { get; set; }

        public ICollection<Vehicle>? Vehicle { get; set; }
    }
}
