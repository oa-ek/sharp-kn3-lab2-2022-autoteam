using AutoOA.Core;
using AutoOA.Repository.Dto.ColorDto;
using AutoOA.Repository.Dto.UserDto;
using AutoOA.Repository.Repositories;
using AutoOA.UI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoOA.UI.Controllers
{
    public class ColorController : Controller
    {
        private readonly ILogger<ColorController> _logger;
        private readonly VehicleColorRepository _vehicleColorRepository;
        public ColorController(ILogger<ColorController> logger, VehicleColorRepository colorRepository)
        {
            _logger = logger;
            _vehicleColorRepository = colorRepository;
        }

        public IActionResult Index()
        {
            return View(_vehicleColorRepository.GetColors());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(VehicleColorCreateDto colorDto)
        {
            if (ModelState.IsValid)
            {
                var color = await _vehicleColorRepository.AddColorAsync(new VehicleColor
                {
                    ColorName = colorDto.ColorName
                });
                return RedirectToAction("Sellcar", "Vehicles");
            }
            return View(colorDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _vehicleColorRepository.GetColorDto(id));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _vehicleColorRepository.GetColorDto(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(VehicleColorReadDto model)
        {
            if (ModelState.IsValid)
            {
                await _vehicleColorRepository.UpdateAsync(model);
                return RedirectToAction("Index", "Color");
            }
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _vehicleColorRepository.DeleteColorAsync(id);

            return RedirectToAction("Index", "Color");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
