using AutoOA.Core;
using AutoOA.Repository.Dto.ColorDto;
using AutoOA.Repository.Dto.VehicleDto;
using Microsoft.EntityFrameworkCore;

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
        public async Task<VehicleColorReadDto> GetColorDto(int id)
        {
            var v = await _ctx.VehicleColors.FirstAsync(x => x.Id == id);

            var colorDto = new VehicleColorReadDto
            {
                Id = v.Id,
                ColorName = v.ColorName
            };
            return colorDto;
        }

        public async Task UpdateAsync(VehicleColorReadDto colorDto)
        {
            var color = _ctx.VehicleColors.FirstOrDefault(x => x.Id == colorDto.Id);

            if (color.ColorName != colorDto.ColorName)
                color.ColorName = colorDto.ColorName;
            
            _ctx.SaveChanges();
        }

        public async Task DeleteColorAsync(int id)
        {
            _ctx.Remove(GetColor(id));
            await _ctx.SaveChangesAsync();
        }
    }
}
