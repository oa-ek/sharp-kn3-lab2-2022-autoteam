using AutoOA.Core;
using AutoOA.Repository.Dto.BodyTypeDto;
using AutoOA.Repository.Dto.ColorDto;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Drawing;

namespace AutoOA.Repository.Repositories
{
    public class BodyTypeRepository
    {
        private readonly AutoOADbContext _ctx;

        public BodyTypeRepository(AutoOADbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<BodyType> AddBodyTypeAsync(BodyType type)
        {
            _ctx.BodyTypes.Add(type);
            await _ctx.SaveChangesAsync();
            return _ctx.BodyTypes.FirstOrDefault(x => x.BodyTypeName == type.BodyTypeName);
        }

        public List<BodyType> GetBodyTypes()
        {
            var bodyList = _ctx.BodyTypes.ToList();
            return bodyList;
        }

        public BodyType GetBodyType(int id)
        {
            return _ctx.BodyTypes.FirstOrDefault(x => x.BodyTypeId == id);
        }

        public BodyType GetBodyTypeByName(string name)
        {
            return _ctx.BodyTypes.FirstOrDefault(x => x.BodyTypeName == name);
        }

        public async Task<BodyTypeReadDto> GetBodyTypeDto(int id)
        {
            var v = await _ctx.BodyTypes.FirstAsync(x => x.BodyTypeId == id);

            var bodyTypeDto = new BodyTypeReadDto
            {
                BodyId = v.BodyTypeId,
                BodyName = v.BodyTypeName,
            };
            return bodyTypeDto;
        }

        public async Task UpdateAsync(BodyTypeReadDto bodyTypeDto)
        {
            var bodyType = _ctx.BodyTypes.FirstOrDefault(x => x.BodyTypeId == bodyTypeDto.BodyId);

            if (bodyType.BodyTypeName != bodyTypeDto.BodyName)
                bodyType.BodyTypeName = bodyTypeDto.BodyName;

            _ctx.SaveChanges();
        }

        public async Task DeleteBodyTypeAsync(int id)
        {
            _ctx.Remove(GetBodyType(id));
            await _ctx.SaveChangesAsync();
        }        
    }
}
