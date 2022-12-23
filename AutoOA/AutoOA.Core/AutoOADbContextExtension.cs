using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoOA.Core
{
    public static class AutoOADbContextExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            string MODER_ROLE_ID = Guid.NewGuid().ToString();
            string USER_ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = MODER_ROLE_ID,
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },
                new IdentityRole
                {
                    Id = USER_ROLE_ID,
                    Name = "User",
                    NormalizedName = "USER"
                });

            string ADMIN_ID = Guid.NewGuid().ToString();
            string MODER_ID = Guid.NewGuid().ToString();
            string USER_ID = Guid.NewGuid().ToString();

            var admin = new User
            {
                Id = ADMIN_ID,
                UserName = "admin@autooa.com",
                Email = "admin@autooa.com",
                EmailConfirmed = true,
                NormalizedEmail = "admin@autooa.com".ToUpper(),
                NormalizedUserName = "admin@autooa.com".ToUpper()
            };

            var moder = new User
            {
                Id = MODER_ID,
                UserName = "moder@autooa.com",
                Email = "moder@autooa.com",
                EmailConfirmed = true,
                NormalizedEmail = "moder@autooa.com".ToUpper(),
                NormalizedUserName = "moder@autooa.com".ToUpper()
            };

            var user = new User
            {
                Id = USER_ID,
                UserName = "user@autooa.com",
                Email = "user@autooa.com",
                EmailConfirmed = true,
                NormalizedEmail = "user@autooa.com".ToUpper(),
                NormalizedUserName = "user@autooa.com".ToUpper(),
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin$pass1");
            moder.PasswordHash = hasher.HashPassword(moder, "Moder$pass1");
            user.PasswordHash = hasher.HashPassword(user, "User$pass1");

            builder.Entity<User>().HasData(admin, moder, user);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = MODER_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = MODER_ROLE_ID,
                    UserId = MODER_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = MODER_ID
                },
                new IdentityUserRole<string>
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                });

            builder.Entity<FuelType>().HasData(
                new FuelType
                {
                    FuelTypeId = 1,
                    FuelName = "Дизель",
                },
                new FuelType
                {
                    FuelTypeId = 2,
                    FuelName = "Бензин",
                },
                new FuelType
                {
                    FuelTypeId = 3,
                    FuelName = "Газ",
                },
                new FuelType
                {
                    FuelTypeId = 4,
                    FuelName = "Газ / Бензин",
                },
                new FuelType
                {
                    FuelTypeId = 5,
                    FuelName = "Гібрид",
                },
                new FuelType
                {
                    FuelTypeId = 6,
                    FuelName = "Електро",
                },
                new FuelType
                {
                    FuelTypeId = 7,
                    FuelName = "Інше",
                },
                new FuelType
                {
                    FuelTypeId = 8,
                    FuelName = "Газ метан",
                },
                new FuelType
                {
                    FuelTypeId = 9,
                    FuelName = "Газ пропан-бутан",
                });
            builder.Entity<BodyType>().HasData(
                new BodyType
                {
                    BodyTypeId = 1,
                    BodyTypeName = "Седан",
                },
                new BodyType
                {
                    BodyTypeId = 2,
                    BodyTypeName = "Універсал",
                },
                new BodyType
                {
                    BodyTypeId = 3,
                    BodyTypeName = "Хетчбек",
                },
                new BodyType
                {
                    BodyTypeId = 4,
                    BodyTypeName = "Купе",
                },
                new BodyType
                {
                    BodyTypeId = 5,
                    BodyTypeName = "Кабріолет",
                },
                new BodyType
                {
                    BodyTypeId = 6,
                    BodyTypeName = "Позашляховик / Кросовер",
                },
                new BodyType
                {
                    BodyTypeId = 7,
                    BodyTypeName = "Мінівен",
                },
                new BodyType
                {
                    BodyTypeId = 8,
                    BodyTypeName = "Ліфтбек",
                },
                new BodyType
                {
                    BodyTypeId = 9,
                    BodyTypeName = "Мікровен",
                },
                new BodyType
                {
                    BodyTypeId = 10,
                    BodyTypeName = "Пікап",
                },
                new BodyType
                {
                    BodyTypeId = 11,
                    BodyTypeName = "Родстер",
                },
                new BodyType
                {
                    BodyTypeId = 12,
                    BodyTypeName = "Лімузин",
                },
                new BodyType
                {
                    BodyTypeId = 13,
                    BodyTypeName = "Фастбек",
                });
            builder.Entity<DriveType>().HasData(
                new DriveType
                {
                    DriveTypeId = 1,
                    DriveTypeName = "Повний",
                },
                new DriveType
                {
                    DriveTypeId = 2,
                    DriveTypeName = "Передній",
                },
                new DriveType
                {
                    DriveTypeId = 3,
                    DriveTypeName = "Задній",
                });
            builder.Entity<GearBox>().HasData(
                new GearBox
                {
                    GearBoxId = 1,
                    GearBoxName = "Ручна / Механіка",
                },
                new GearBox
                {
                    GearBoxId = 2,
                    GearBoxName = "Автомат",
                },
                new GearBox
                {
                    GearBoxId = 3,
                    GearBoxName = "Типтронік",
                },
                new GearBox
                {
                    GearBoxId = 4,
                    GearBoxName = "Робот",
                },
                new GearBox
                {
                    GearBoxId = 5,
                    GearBoxName = "Варіатор",
                });
            builder.Entity<VehicleBrand>().HasData(
                new VehicleBrand
                {
                    VehicleBrandId = 1,
                    VehicleBrandName = "Mercedes",
                },
                new VehicleBrand
                {
                    VehicleBrandId = 2,
                    VehicleBrandName = "Bmw",
                },
                new VehicleBrand
                {
                    VehicleBrandId = 3,
                    VehicleBrandName = "Acura",
                },
                new VehicleBrand
                {
                    VehicleBrandId = 4,
                    VehicleBrandName = "Alfa Romeo",
                },
                new VehicleBrand
                {
                    VehicleBrandId = 5,
                    VehicleBrandName = "Alphina",
                },
                new VehicleBrand
                {
                    VehicleBrandId = 6,
                    VehicleBrandName = "Audi"
                });
            builder.Entity<VehicleModel>().HasData(
                new VehicleModel
                {
                    VehicleModelId = 1,
                    VehicleModelName = "E 220",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 2,
                    VehicleModelName = "320",
                    VehicleBrandId = 2,
                },
                new VehicleModel
                {
                    VehicleModelId = 3,
                    VehicleModelName = "190",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 4,
                    VehicleModelName = "230 Pullman",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 5,
                    VehicleModelName = "A-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 6,
                    VehicleModelName = "AMG GT",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 7,
                    VehicleModelName = "AMG GT 4-Door Coupe",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 8,
                    VehicleModelName = "B-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 9,
                    VehicleModelName = "C-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 10,
                    VehicleModelName = "C-Class All-Terrain",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 11,
                    VehicleModelName = "Citan",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 12,
                    VehicleModelName = "CL-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 13,
                    VehicleModelName = "CLA-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 14,
                    VehicleModelName = "CLC-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 15,
                    VehicleModelName = "CLK-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 16,
                    VehicleModelName = "CLS-Class",
                    VehicleBrandId = 1,
                },
                new VehicleModel
                {
                    VehicleModelId = 17,
                    VehicleModelName = "G-Class",
                    VehicleBrandId = 1,
                });
            builder.Entity<SalesData>().HasData(
                new SalesData
                {
                    SalesDataId = 1,
                    VehicleId = 1,
                    CreatedOn = DateTime.Now,
                },
                new SalesData
                {
                    SalesDataId = 2,
                    VehicleId = 2,
                    CreatedOn = new DateTime(2022, 10, 10)
                });
            builder.Entity<Region>().HasData(
                new Region
                {
                    RegionId = 1,
                    RegionName = "Україна"
                },
                new Region
                {
                    RegionId = 2,
                    RegionName = "США"
                },
                new Region
                {
                    RegionId = 3,
                    RegionName = "Китай"
                },
                new Region
                {
                    RegionId = 4,
                    RegionName = "Великобританія"
                },
                new Region
                {
                    RegionId = 5,
                    RegionName = "Корея"
                });
            builder.Entity<VehicleColor>().HasData(
                new VehicleColor
                {
                    Id = 1,
                    ColorName = "Бежевий"
                },
                new VehicleColor
                {
                    Id = 2,
                    ColorName = "Чорний"
                },
                new VehicleColor
                {
                    Id = 3,
                    ColorName = "Синій"
                },
                new VehicleColor
                {
                    Id = 4,
                    ColorName = "Коричневий"
                },
                new VehicleColor
                {
                    Id = 5,
                    ColorName = "Зелений"
                },
                new VehicleColor
                {
                    Id = 6,
                    ColorName = "Сірий"
                },
                new VehicleColor
                {
                    Id = 7,
                    ColorName = "Помаранчевий"
                },
                new VehicleColor
                {
                    Id = 8,
                    ColorName = "Фіолетовий"
                },
                new VehicleColor
                {
                    Id = 9,
                    ColorName = "Червоний"
                },
                new VehicleColor
                {
                    Id = 10,
                    ColorName = "Білий"
                },
                new VehicleColor
                {
                    Id = 11,
                    ColorName = "Жовтий"
                });
           builder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    VehicleId = 1,
                    BodyTypeId = 1,
                    DriveTypeId = 1,
                    VehicleModelId = 1,
                    ProductionYear = 2006,
                    GearBoxId = 1,
                    Price_USD = 19000,
                    Price_UAH = 698187.11m,
                    Price_EUR = 18968.65m,
                    RegionId = 1,
                    NumberOfDoors = 5,
                    NumberOfSeats = 4,
                    isNew = true,
                    Mileage = 90000,
                    VehicleIconPath = @"\Images\w220cidan.png",
                    FuelTypeId = 1,
                    UserId = USER_ID,
                    VehicleColorId = 2,
                    Description = "Авто в дуже хорошому стані. Повністтю обслужене. Капіталовкладень не потребує."
                },
                new Vehicle
                {
                    VehicleId = 2,
                    BodyTypeId = 2,
                    DriveTypeId = 3,
                    VehicleModelId = 2,
                    ProductionYear = 2000,
                    GearBoxId = 2,
                    Price_USD = 15000,
                    Price_UAH = 551200.35m,
                    Price_EUR = 14976.07m,
                    RegionId = 1,
                    NumberOfDoors = 5,
                    NumberOfSeats = 4,
                    isNew = false, 
                    Mileage = 320000,
                    VehicleIconPath = @"\Images\320Universal.png",
                    FuelTypeId = 2,
                    UserId = USER_ID,
                    VehicleColorId = 3,
                    Description = "Продаю свій автомобіль у хорошому стані. Зроблено всі планові роботи, все працює добре. Є невеликі подряпини, пов'язані з експлуатацією."
                });
        }
    }
}
