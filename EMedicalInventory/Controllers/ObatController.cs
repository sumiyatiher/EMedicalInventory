using EMedicalInventory.Models;
using EMedicalInventory.Repo.ObatRepos;
using EMedicalInventory.ViewModels.Obat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EMedicalInventory.Controllers
{
    public class ObatController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly IObatRepo _obatRepo;

        public ObatController( UserManager<Users> userManager,IObatRepo obatRepo)
        {
            _userManager = userManager;
            _obatRepo = obatRepo;
        }

        public async Task<IActionResult> Index()
        {
            var obatList = await _obatRepo.GetAllDataAsync();
            return View(obatList);
        }

        [HttpGet]
        public IActionResult AddDrug()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ObatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(User);

                    await _obatRepo.AddObatAsync(model, userId);

                    return RedirectToAction("Index", "Obat");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Terjadi kesalahan saat menyimpan data: {ex.Message}");

                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditDrug()
        {
            return View();
        }
    }
}
