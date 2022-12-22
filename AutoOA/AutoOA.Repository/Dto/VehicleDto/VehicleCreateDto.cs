using AutoOA.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Repository.Dto.VehicleDto
{
    public class VehicleCreateDto
    {
        [Required]
        public string? RegionName { get; set; }

        [Required]
        public string? BodyTypeName { get; set; }

        [Required]
        public string? VehicleModelName { get; set; }

        [Required]
        public string? DriveTypeName { get; set; }

        [RegularExpression("^[A-Za-z]{2}[0-9]{4}[A-Za-z]{2}$", ErrorMessage = "Must be a valid state number")]
        public string? StateNumber { get; set; } = "Не задано";

        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Must be a valid year")]
        public short ProductionYear { get; set; }

        [Required]
        public string? VehicleBrandName { get; set; }

        [Required]
        public string? GearBoxName { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Must be a valid number")]
        public int NumberOfSeats { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Must be a valid number")]
        public int NumberOfDoors { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Must be a valid Price")]
        public decimal Price_USD { get; set; }

        public decimal Price_UAH { get; set; }
        public decimal Price_EUR { get; set; }

        [Required]
        public bool isNew { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Must be a valid mileage")]
        public int Mileage { get; set; }

        
        public string? VehicleIconPath { get; set; }

        [Required]
        public string? FuelTypeName { get; set; }


        public string? VehicleColor { get; set; }

        [Required]
        public string? Description { get; set; }

        public SalesData? SalesData { get; set; }

        public string? UserId { get; set; }
    }
}
