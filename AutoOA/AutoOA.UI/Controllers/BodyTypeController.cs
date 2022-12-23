using AutoOA.Core;
using AutoOA.Repository.Dto.BodyTypeDto;
using AutoOA.Repository.Dto.ColorDto;
using AutoOA.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoOA.UI.Controllers
{
    public class BodyTypeController : Controller
    {
        private readonly ILogger<BodyTypeController> _logger;
        private readonly BodyTypeRepository _bodyTypeRepository;
        public BodyTypeController(ILogger<BodyTypeController> logger, BodyTypeRepository bodyTypeRepository)
        {
            _logger = logger;
            _bodyTypeRepository = bodyTypeRepository;
        }

        public IActionResult Index()
        {
            return View(_bodyTypeRepository.GetBodyTypes());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _bodyTypeRepository.GetBodyTypeDto(id));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _bodyTypeRepository.GetBodyTypeDto(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(BodyTypeReadDto model)
        {
            if (ModelState.IsValid)
            {
                await _bodyTypeRepository.UpdateAsync(model);
                return RedirectToAction("Index", "BodyType");
            }
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _bodyTypeRepository.DeleteBodyTypeAsync(id);

            return RedirectToAction("Index", "BodyType");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(BodyTypeCreateDto bodyTypeDto)
        {
            if (ModelState.IsValid)
            {
                var bodyType = await _bodyTypeRepository.AddBodyTypeAsync(new BodyType
                {
                    BodyTypeName = bodyTypeDto.BodyName
                });
                return RedirectToAction("Sellcar", "Vehicles");
            }
            return View(bodyTypeDto);
        }
    }
}
