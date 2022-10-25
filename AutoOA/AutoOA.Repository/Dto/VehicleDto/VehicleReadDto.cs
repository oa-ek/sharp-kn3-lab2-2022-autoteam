﻿using AutoOA.Core;

namespace AutoOA.Repository.Dto.VehicleDto
{
    public class VehicleReadDto
    {
        public int VehicleId { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public string? RegionName { get; set; }
        public string? BodyTypeIconPath { get; set; }
        public string? BodyTypeName { get; set; }
        public string? VehicleModelName { get; set; }
        public string? DriveTypeName { get; set; }
        public string? StateNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public string? VehicleBrandName { get; set; }
        public string? GearBoxIconPath { get; set; }
        public string? GearBoxName { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfDoors { get; set; }
        public decimal Price { get; set; }
        public bool isNew { get; set; }
        public string? MileageIconPath { get; set; }
        public int? Mileage { get; set; }
        public string? VehicleIconPath { get; set; }
        public string? FuelIconPath { get; set; }
        public string? FuelTypeName { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }

        public SalesData? SalesData { get; set; }
    }
}
