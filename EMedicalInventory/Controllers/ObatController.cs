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

        public ObatController(UserManager<Users> userManager, IObatRepo obatRepo)
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

                    TempData["SuccessMessage"] = "Drug Added successfully!";

                    return RedirectToAction("Index", "Obat");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while Adding the drug.";

                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditDrug(int id)
        {
            var obat = await _obatRepo.GetByIdAsync(id);
            if (obat == null)
            {
                return NotFound();
            }


            return View(obat);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateObat(ObatViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                await _obatRepo.UpdateAsync(model, userId);

                TempData["SuccessMessage"] = "Drug updated successfully!";

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the drug.";
            }

            return RedirectToAction("Index", "Obat");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteDrug(int id)
        {
            try
            {
                await _obatRepo.DeleteAsync(id);
                TempData["SuccessMessage"] = "Drug deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the drug.";
            }

            return RedirectToAction("Index");
        }
    }
}
