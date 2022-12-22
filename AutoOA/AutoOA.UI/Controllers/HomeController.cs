using AutoOA.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoOA.Repository.Repositories;
using AutoOA.Core;
using Microsoft.EntityFrameworkCore;
using AutoOA.Repository.Dto.VehicleDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace AutoOA.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        private readonly AutoOADbContext _ctx;

        public HomeController(ILogger<HomeController> logger, VehicleRepository vehicleRepository,
            RegionRepository regionRepository, VehicleModelRepository vehicleModelRepository,
            VehicleBrandRepository vehicleBrandRepository, FuelTypeRepository fuelTypeRepository,
            GearBoxRepository gearBoxRepository, AutoOADbContext ctx,
            DriveTypeRepository driveTypeRepository, BodyTypeRepository bodyTypeRepository,
            SalesDataRepository salesDataRepository, UsersRepository usersRepository,
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
            _vehicleColorRepository = vehicleColorRepository;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View(_vehicleRepository.GetVehicles());
        }

        public IActionResult IndexUAH()
        {
            return View(_vehicleRepository.GetVehicles());
        }

        public IActionResult IndexEUR()
        {
            return View(_vehicleRepository.GetVehicles());
        }

        [HttpPost]
        public async Task<IActionResult> Filter(int? regionId, int? brandId, int? modelId, int? bodyTypeId, int? colorId)
        {
            GetViewBags();

            var result = _vehicleRepository.GetVehicles();

            if (regionId != null)
            {
                result = result.Where(x => x.RegionId == regionId).ToList();
            }


            if ( modelId != null)
            {
                result = result.Where(x => x.VehicleModel.VehicleModelId == modelId).ToList();
            }


            if (brandId != null)
            {
                result = result.Where(x => x.VehicleModel.VehicleBrandId == brandId).ToList();
            }


            if (colorId != null)
            {
                result = result.Where(x => x.VehicleColorId == colorId).ToList();
            }


            if (bodyTypeId != null)
            {
                result = result.Where(x => x.BodyTypeId == bodyTypeId).ToList();
            }

            return View("Index", result);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString)
        {
            GetViewBags();

            ViewData["GetVehicleDetails"] = searchString;

            var brands = from b in _ctx.Vehicles
                         select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                brands = brands.Where(s => s.VehicleModel.VehicleBrand.VehicleBrandName.Contains(searchString));
            }
            return View(await brands.Include(x => x.VehicleModel).ThenInclude(x => x.VehicleBrand).
                Include(x => x.BodyType).
                Include(x => x.DriveType).
                Include(x => x.FuelType).
                Include(x => x.GearBox).
                Include(x => x.Region).
                Include(x => x.User).
                Include(x => x.VehicleColor).
                Include(x => x.SalesData).ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetViewBags()
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
    }
}