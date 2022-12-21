using AutoOA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOA.Repository.Repositories
{
    public class VehicleColorRepository
    {
        private readonly AutoOADbContext _ctx;

        public VehicleColorRepository(AutoOADbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<VehicleColor> AddColorAsync(VehicleColor color)
        {
            _ctx.VehicleColors.Add(color);
            await _ctx.SaveChangesAsync();
            return _ctx.VehicleColors.FirstOrDefault(x => x.ColorName == color.ColorName);
        }

        public List<VehicleColor> GetColors()
        {
            var colorList = _ctx.VehicleColors.ToList();
            return colorList;
        }

        public VehicleColor GetColor(int id)
        {
            return _ctx.VehicleColors.FirstOrDefault(x => x.Id == id);
        }

        public VehicleColor GetColorByName(string name)
        {
            return _ctx.VehicleColors.FirstOrDefault(x => x.ColorName == name);
        }

        public VehicleColor GetColorByMetallic(bool isMetallic)
        {
            return _ctx.VehicleColors.FirstOrDefault(x => x.isMetallic == isMetallic);
        }

        public async Task DeleteColorAsync(int id)
        {
            _ctx.Remove(GetColor(id));
            await _ctx.SaveChangesAsync();
        }
    }
}
