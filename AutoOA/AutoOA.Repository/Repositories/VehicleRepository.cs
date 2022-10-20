﻿using AutoOA.Core;
using AutoOA.Repository.Dto.VehicleDto;

namespace AutoOA.Repository.Repositories
{
    public class VehicleRepository
    {
        private readonly AutoOADbContext _ctx;

        public VehicleRepository(AutoOADbContext ctx, VehicleModelRepository vehicleModelRepository, BodyTypeRepository bodyTypeRepository)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<VehicleReadDto>> GetVehicleAsync()
        {
            var vehicleDto  = _ctx.Vehicles
                .Select(x => new VehicleReadDto{ 
                    BodyType = x.BodyType, 
                    VehicleModel = x.VehicleModel,
                    GearBox = x.GearBox, 
                    VehicleBrand = x.VehicleModel.VehicleBrand.VehicleBrandName,
                    Price = x.Price, 
                    isNew = x.isNew, 
                    Mileage = x.Mileage, 
                    IconPath = x.IconPath, 
                    FuelType = x.FuelType, 
                    Color = x.Color }).ToList();

            return vehicleDto;
        }
    }
}