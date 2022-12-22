using AutoOA.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoOA.Repository.Repositories;
using AutoOA.Repository.Dto.VehicleDto;
using AutoOA.Core;
using Microsoft.AspNetCore.Identity;

namespace AutoOA.UI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ILogger<VehiclesController> _logger;

        private readonly VehicleRepository _vehicleRepository;
        private readonly RegionRepository _regionRepository;
        private readonly VehicleModelRepository _vehicleModelRepository;
        private readonly VehicleBrandRepository _vehicleBrandRepository;
        private readonly FuelTypeRepository _fuelTypeRepository;
        private readonly GearBoxRepository _gearBoxRepository;
        private readonly DriveTypeRepository _driveTypeRepository;
        private readonly BodyTypeRepository _bodyTypeRepository;
        private readonly SalesDataRepository _salesDataRepository;
        private readonly UsersRepository _usersRepository;
        private readonly VehicleColorRepository _vehicleColorRepository;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehiclesController(ILogger<VehiclesController> logger, VehicleRepository vehicleRepository,
            RegionRepository regionRepository, VehicleModelRepository vehicleModelRepository,
            VehicleBrandRepository vehicleBrandRepository, FuelTypeRepository fuelTypeRepository,
            GearBoxRepository gearBoxRepository, SignInManager<User> signInManager,
            DriveTypeRepository driveTypeRepository, BodyTypeRepository bodyTypeRepository,
            SalesDataRepository salesDataRepository, UsersRepository usersRepository,
            UserManager<User> userManager, IWebHostEnvironment webHostEnvironment,
            VehicleColorRepository vehicleColorRepository)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
            _regionRepository = regionRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _vehicleBrandRepository = vehicleBrandRepository;
            _fuelTypeRepository = fuelTypeRepository;
            _gearBoxRepository = gearBoxRepository;
            _driveTypeRepository = driveTypeRepository;
            _bodyTypeRepository = bodyTypeRepository;
            _salesDataRepository = salesDataRepository;
            _usersRepository = usersRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _vehicleColorRepository = vehicleColorRepository;
        }

        public IActionResult Index()
        {
            return View(_vehicleRepository.GetVehicles());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Models = _vehicleModelRepository.GetVehicleModel(id);
            return View(_vehicleRepository.GetVehicle(id));
        }

        [HttpGet]
        public IActionResult Sellcar()
        {
            ViewBag.Regions = _regionRepository.GetRegions();
            ViewBag.Models = _vehicleModelRepository.GetVehicleModels();
            ViewBag.Brands = _vehicleBrandRepository.GetVehicleBrands();
            ViewBag.FuelTypes = _fuelTypeRepository.GetFuelTypes();
            ViewBag.GearBoxes = _gearBoxRepository.GetGearBoxes();
            ViewBag.DriveTypes = _driveTypeRepository.GetDriveTypes();
            ViewBag.BodyTypes = _bodyTypeRepository.GetBodyTypes();
            ViewBag.Colors = _vehicleColorRepository.GetColors();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Sellcar(VehicleCreateDto vehicleDto, IFormFile picture, string regionName, string bodyTypeName,
            string vehicleBrandName, string vehicleModelName, string gearBoxName, string driveTypeName, string fuelTypeName, string colorName)
        {
            
            if (ModelState.IsValid)
            {
                string picturePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", picture.FileName);

                using (FileStream stream = new FileStream(picturePath, FileMode.Create))
                    picture.CopyTo(stream);
                string url = @"\Images\" + picture.FileName;

                vehicleDto.VehicleIconPath = url;

                var region = _regionRepository.GetRegionByName(regionName);
                if (region == null)
                {
                    region = new Region() { RegionName = regionName };
                    region = await _regionRepository.AddRegionAsync(region);
                }

                var bodyType = _bodyTypeRepository.GetBodyTypeByName(bodyTypeName);
                if (bodyType == null)
                {
                    bodyType = new BodyType() { BodyTypeName = bodyTypeName };
                    bodyType = await _bodyTypeRepository.AddBodyTypeAsync(bodyType);
                }

                var vehicleBrand = _vehicleBrandRepository.GetVehicleBrandByName(vehicleBrandName);
                if (vehicleBrand == null)
                {
                    vehicleBrand = new VehicleBrand() { VehicleBrandName = vehicleBrandName };
                    vehicleBrand = await _vehicleBrandRepository.AddVehicleBrandAsync(vehicleBrand);
                }

                var vehicleModel = _vehicleModelRepository.GetVehicleModelByName(vehicleModelName);
                if (vehicleModel == null)
                {
                    vehicleModel = new VehicleModel() { VehicleModelName = vehicleModelName, VehicleBrand = vehicleBrand };
                    vehicleModel = await _vehicleModelRepository.AddVehicleModelAsync(vehicleModel);
                }

                var gearBox = _gearBoxRepository.GetGearBoxByName(gearBoxName);
                if (gearBox == null)
                {
                    gearBox = new GearBox() { GearBoxName = gearBoxName };
                    gearBox = await _gearBoxRepository.AddGearBoxAsync(gearBox);
                }

                var driveType = _driveTypeRepository.GetDriveTypeByName(driveTypeName);
                if (driveType == null)
                {
                    driveType = new Core.DriveType() { DriveTypeName = driveTypeName };
                    driveType = await _driveTypeRepository.AddDriveTypeAsync(driveType);
                }

                var fuelType = _fuelTypeRepository.GetFuelTypeByName(fuelTypeName);
                if (fuelType == null)
                {
                    fuelType = new FuelType() { FuelName = fuelTypeName };
                    fuelType = await _fuelTypeRepository.AddFuelTypeAsync(fuelType);
                }

                var saleData = new SalesData() { CreatedOn = DateTime.Now };

                var user = _usersRepository.GetUserByEmail(User.Identity.Name);
                if (user == null)
                {
                    user = new User() { Email = User.Identity.Name };
                }

                var color = _vehicleColorRepository.GetColorByName(colorName);
                if(color == null)
                {
                    color = new VehicleColor() { ColorName = colorName };
                    color = await _vehicleColorRepository.AddColorAsync(color);
                }

                var vehicle = await _vehicleRepository.AddVehicleAsync(new Vehicle
                {
                    Region = region,
                    BodyType = bodyType,
                    VehicleModel = vehicleModel,
                    ProductionYear = vehicleDto.ProductionYear,
                    GearBox = gearBox,
                    DriveType = driveType,
                    StateNumber = String.IsNullOrEmpty(vehicleDto.StateNumber) ? "Не задано" : vehicleDto.StateNumber.ToUpper(),
                    NumberOfSeats = vehicleDto.NumberOfSeats,
                    NumberOfDoors = vehicleDto.NumberOfDoors,
                    Price_USD = vehicleDto.Price_USD,
                    Price_UAH = vehicleDto.Price_USD * 37,
                    Price_EUR = vehicleDto.Price_USD * 0.9984m,
                    isNew = vehicleDto.isNew,
                    Mileage = vehicleDto.Mileage,
                    VehicleIconPath = vehicleDto.VehicleIconPath,
                    FuelType = fuelType,
                    VehicleColor = color,
                    Description = vehicleDto.Description,
                    SalesData = saleData,
                    User = user
                });

                //vehicle.VehicleIconPath = Path.Combine("img", "upload", picturePath);
                return RedirectToAction("Index", "Home", new { id = vehicle.VehicleId });
            }
            else
            {
                ViewBag.Regions = _regionRepository.GetRegions();
                ViewBag.Models = _vehicleModelRepository.GetVehicleModels();
                ViewBag.Brands = _vehicleBrandRepository.GetVehicleBrands();
                ViewBag.FuelTypes = _fuelTypeRepository.GetFuelTypes();
                ViewBag.GearBoxes = _gearBoxRepository.GetGearBoxes();
                ViewBag.DriveTypes = _driveTypeRepository.GetDriveTypes();
                ViewBag.BodyTypes = _bodyTypeRepository.GetBodyTypes();
                ViewBag.Colors = _vehicleColorRepository.GetColors();
            }
            return View(vehicleDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Regions = _regionRepository.GetRegions();
            ViewBag.Models = _vehicleModelRepository.GetVehicleModels();
            ViewBag.Brands = _vehicleBrandRepository.GetVehicleBrands();
            ViewBag.FuelTypes = _fuelTypeRepository.GetFuelTypes();
            ViewBag.GearBoxes = _gearBoxRepository.GetGearBoxes();
            ViewBag.DriveTypes = _driveTypeRepository.GetDriveTypes();
            ViewBag.BodyTypes = _bodyTypeRepository.GetBodyTypes();
            ViewBag.Colors = _vehicleColorRepository.GetColors();
            return View(await _vehicleRepository.GetVehicleDto(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(VehicleReadDto vehicleDto, string regionName, string bodyTypeName,
            string vehicleBrandName, string vehicleModelName, string gearBoxName, string driveTypeName, string fuelTypeName, string color)
        {
            if (ModelState.IsValid)
            {
                await _vehicleRepository.UpdateAsync(vehicleDto, regionName, bodyTypeName, vehicleBrandName, vehicleModelName, gearBoxName, driveTypeName, fuelTypeName, color);
                return RedirectToAction("Details", "Vehicles", new { id = vehicleDto.Id });
            }
            ViewBag.Regions = _regionRepository.GetRegions();
            ViewBag.Models = _vehicleModelRepository.GetVehicleModels();
            ViewBag.Brands = _vehicleBrandRepository.GetVehicleBrands();
            ViewBag.FuelTypes = _fuelTypeRepository.GetFuelTypes();
            ViewBag.GearBoxes = _gearBoxRepository.GetGearBoxes();
            ViewBag.DriveTypes = _driveTypeRepository.GetDriveTypes();
            ViewBag.BodyTypes = _bodyTypeRepository.GetBodyTypes();
            ViewBag.Colors = _vehicleColorRepository.GetColors();
            return View(vehicleDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _vehicleRepository.GetVehicleDto(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var vehicle = _vehicleRepository.GetVehicle(id);

            string fullPath = _webHostEnvironment.WebRootPath + vehicle.VehicleIconPath;

            if (!System.IO.File.Exists(fullPath)) return View();

            try
            {
                System.IO.File.Delete(fullPath);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
  
            await _vehicleRepository.DeleteVehicleAsync(id);

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}