using AutoOA.Core;

namespace AutoOA.Repository.Dto.VehicleDto
{
    public class VehicleReadDto
    {
        public int Id { get; set; }
        public string? RegionName { get; set; }
        public string? BodyTypeName { get; set; }
        public string? VehicleModelName { get; set; }
        public string? DriveTypeName { get; set; }
        public string? StateNumber { get; set; }
        public short ProductionYear { get; set; }
        public string? VehicleBrandName { get; set; }
        public string? GearBoxName { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfDoors { get; set; }
        public decimal Price_USD { get; set; }
        public decimal Price_UAH { get; set; } 
        public decimal Price_EUR { get; set; }
        public bool isNew { get; set; }
        public int Mileage { get; set; }
        public string? VehicleIconPath { get; set; }
        public string? FuelTypeName { get; set; }
        public string? VehicleColor { get; set; }
        public string? Description { get; set; }
        
        public SalesData? SalesData { get; set; }

        public string? UserId { get; set; }
    }
}
