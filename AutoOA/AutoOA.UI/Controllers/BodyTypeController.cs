using AutoOA.Core;
using AutoOA.Repository.Dto.BodyTypeDto;
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
