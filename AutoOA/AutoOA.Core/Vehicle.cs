using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoOA.Core
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        public int RegionId { get; set; }
        public Region? Region { get; set; } // Регіон

        public int BodyTypeId { get; set; }
        public BodyType? BodyType { get; set; } // Тип кузова

        public int VehicleModelId { get; set; }
        public VehicleModel? VehicleModel { get; set; } // Модель

        public short ProductionYear { get; set; } // Рік випуску

        public int GearBoxId { get; set; }
        public GearBox? GearBox { get; set; } // Коробка передач

        public int DriveTypeId { get; set; }
        public DriveType? DriveType { get; set; } // Тип приводу

        public string? StateNumber { get; set; } = "Не задано"; // Гос номер
        public int NumberOfSeats { get; set; } // Кількість сидінь
        public int NumberOfDoors { get; set; } // Кількість дверей

        [DataType(DataType.Currency)]
        public decimal Price_USD { get; set; } = 0; // Ціна в доларах

        [DataType(DataType.Currency)]
        public decimal Price_UAH { get; set; } = 0; // Ціна в гривнях

        [DataType(DataType.Currency)]
        public decimal Price_EUR { get; set; } = 0; // Ціна в євро
        public bool isNew { get; set; } // Новий?
        public string MileageIconPath { get; set; } = @"\Images\MileageIcon.png";
        public int Mileage { get; set; } // Пробіг

        public string? VehicleIconPath { get; set; } // Шлях до іконки

        public int FuelTypeId { get; set; }
        public FuelType? FuelType { get; set; } // Тип палива
        
        public int VehicleColorId { get; set; }
        public VehicleColor? VehicleColor { get; set; } // Колір

        public string? Description { get; set; } // Опис

        public SalesData? SalesData { get; set; } // День продажу

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
