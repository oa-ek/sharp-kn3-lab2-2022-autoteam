using AutoOA.Core;
using AutoOA.Repository.Dto.VehicleDto;
using Microsoft.EntityFrameworkCore;
using AutoOA.Repository.Repositories;
using Microsoft.AspNetCore.Hosting.Server;

namespace AutoOA.Repository.Repositories
{
    public class VehicleRepository
    {
        private readonly AutoOADbContext _ctx;

        public VehicleRepository(AutoOADbContext ctx)
        {
            _ctx = ctx;
        }
     
        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _ctx.Vehicles.Add(vehicle);
            await _ctx.SaveChangesAsync();
            return _ctx.Vehicles.Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand).
                Include(x => x.BodyType).
                Include(x => x.DriveType).
                Include(x => x.FuelType).
                Include(x => x.GearBox).
                Include(x => x.Region).
                Include(x => x.User).
                Include(x => x.SalesData).
                Include(x => x.VehicleColor).
                FirstOrDefault(x => x.VehicleModel.VehicleModelName == vehicle.VehicleModel.VehicleModelName);
        }

        public Vehicle GetVehicle(int id)
        {
            return _ctx.Vehicles.Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand).
                Include(x => x.BodyType).
                Include(x => x.DriveType).
                Include(x => x.FuelType).
                Include(x => x.GearBox).
                Include(x => x.Region).
                Include(x => x.User).
                Include(x => x.SalesData).
                Include(x => x.VehicleColor).
                FirstOrDefault(x => x.VehicleId == id);
        }

        public List<Vehicle> GetVehicles()
        {
            var vehicleList = _ctx.Vehicles.Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand).
                 Include(x => x.BodyType).
                 Include(x => x.DriveType).
                 Include(x => x.FuelType).
                 Include(x => x.GearBox).
                 Include(x => x.Region).
                 Include(x => x.User).
                 Include(x => x.VehicleColor).
                 Include(x => x.SalesData).ToList();

            return vehicleList;
        }

        public async Task<VehicleReadDto> GetVehicleDto(int id)
        {
            var v = await _ctx.Vehicles.Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand).
                 Include(x => x.BodyType).
                 Include(x => x.DriveType).
                 Include(x => x.FuelType).
                 Include(x => x.GearBox).
                 Include(x => x.Region).
                 Include(x => x.User).
                 Include(x => x.VehicleColor).
                 Include(x => x.SalesData).FirstAsync(x => x.VehicleId == id);

            var vehicleDto = new VehicleReadDto
            {
                Id = v.VehicleId,
                RegionName = v.Region.RegionName,
                BodyTypeName = v.BodyType.BodyTypeName,
                VehicleModelName = v.VehicleModel.VehicleModelName,
                DriveTypeName = v.DriveType.DriveTypeName,
                StateNumber = v.StateNumber,
                ProductionYear = v.ProductionYear,
                VehicleBrandName = v.VehicleModel.VehicleBrand.VehicleBrandName,
                GearBoxName = v.GearBox.GearBoxName,
                NumberOfSeats = v.NumberOfSeats,
                NumberOfDoors = v.NumberOfDoors,
                Price_USD = v.Price_USD,
                Price_UAH = v.Price_UAH,
                Price_EUR = v.Price_EUR,
                isNew = v.isNew,
                Mileage = v.Mileage,
                VehicleIconPath = v.VehicleIconPath,
                FuelTypeName = v.FuelType.FuelName,
                VehicleColor = v.VehicleColor.ColorName,
                Description = v.Description,
                SalesData = v.SalesData,
                UserId = v.UserId,
            };
            return vehicleDto;
        }

        public async Task UpdateAsync(VehicleReadDto vehicleDto, string regionName, string bodyTypeName,
            string vehicleBrandName, string vehicleModelName, string gearBoxName, string driveTypeName,
            string fuelTypeName, string colorName )
        {
            var vehicle = _ctx.Vehicles.Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand).
                 Include(x => x.BodyType).
                 Include(x => x.DriveType).
                 Include(x => x.FuelType).
                 Include(x => x.GearBox).
                 Include(x => x.Region).
                 Include(x => x.User).
                 Include(x => x.VehicleColor).
                 Include(x => x.SalesData).FirstOrDefault(x => x.VehicleId == vehicleDto.Id);

            if (vehicle.Region.RegionName != regionName)
                vehicle.Region = _ctx.Regions.FirstOrDefault(x => x.RegionName == regionName);
            if (vehicle.BodyType.BodyTypeName != bodyTypeName)
                vehicle.BodyType = _ctx.BodyTypes.FirstOrDefault(x => x.BodyTypeName == bodyTypeName);
            if (vehicle.VehicleModel.VehicleModelName != vehicleModelName)
                vehicle.VehicleModel = _ctx.VehicleModels.FirstOrDefault(x => x.VehicleModelName == vehicleModelName);
            if (vehicle.DriveType.DriveTypeName != driveTypeName)
                vehicle.DriveType= _ctx.DriveTypes.FirstOrDefault(x => x.DriveTypeName == driveTypeName);
            if (vehicle.StateNumber != vehicleDto.StateNumber)
                vehicle.StateNumber = vehicleDto.StateNumber.ToUpper();
            if (vehicle.ProductionYear != vehicleDto.ProductionYear)
                vehicle.ProductionYear = vehicleDto.ProductionYear;
            if (vehicle.VehicleModel.VehicleBrand.VehicleBrandName != vehicleBrandName)
                vehicle.VehicleModel.VehicleBrand = _ctx.VehicleBrands.FirstOrDefault(x => x.VehicleBrandName == vehicleBrandName);
            if (vehicle.GearBox.GearBoxName != gearBoxName)
                vehicle.GearBox = _ctx.GearBoxes.FirstOrDefault(x => x.GearBoxName == gearBoxName);
            if (vehicle.NumberOfSeats != vehicleDto.NumberOfSeats)
                vehicle.NumberOfSeats = vehicleDto.NumberOfSeats;
            if (vehicle.NumberOfDoors != vehicleDto.NumberOfDoors)
                vehicle.NumberOfDoors = vehicleDto.NumberOfDoors;
            if (vehicle.Price_USD != vehicleDto.Price_USD)
                vehicle.Price_USD = vehicleDto.Price_USD;
            if (vehicle.Price_UAH != vehicleDto.Price_UAH)
                vehicle.Price_UAH = vehicleDto.Price_USD * 37;
            if (vehicle.Price_EUR != vehicleDto.Price_EUR)
                vehicle.Price_EUR = vehicleDto.Price_USD * 0.9984m;
            if (vehicle.isNew != vehicleDto.isNew)
                vehicle.isNew = vehicleDto.isNew;
            if (vehicle.Mileage != vehicleDto.Mileage)
                vehicle.Mileage = vehicleDto.Mileage;
            if (vehicle.VehicleIconPath != vehicleDto.VehicleIconPath)
                vehicle.VehicleIconPath = vehicleDto.VehicleIconPath;
            if (vehicle.FuelType.FuelName != fuelTypeName)
                vehicle.FuelType = _ctx.FuelTypes.FirstOrDefault(x => x.FuelName == fuelTypeName);
            if (vehicle.VehicleColor.ColorName != colorName)
                vehicle.VehicleColor = _ctx.VehicleColors.FirstOrDefault(x => x.ColorName == colorName);
            if (vehicle.Description != vehicleDto.Description)
                vehicle.Description = vehicleDto.Description;

            vehicle.SalesData.UpdatedOn = DateTime.Now;
            _ctx.SaveChanges();
        }

        public async Task DeleteVehicleAsync(int id)
        {
            _ctx.Remove(GetVehicle(id));
            await _ctx.SaveChangesAsync();
        }
    }
}
