using EMedicalInventory.Models;
using EMedicalInventory.Repo.ObatRepos;
using EMedicalInventory.Repo.RequestRepo;
using EMedicalInventory.ViewModels.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMedicalInventory.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestRepo _requestRepo;
        private readonly IObatRepo _obatRepo;
        private readonly UserManager<Users> _userManager;

        public RequestController(IRequestRepo requestRepo, IObatRepo obatRepo , UserManager<Users> userManager)
        {
            _requestRepo = requestRepo;
            _userManager = userManager;
            _obatRepo = obatRepo;
        }
        public async Task<IActionResult> Index(string searchTerm, string sortColumn, string sortOrder, int page = 1, int pageSize = 5)
        {
            var userId = _userManager.GetUserId(User);
            var model = await _requestRepo.GetRequestObatListViewModelAsync(searchTerm, sortColumn, sortOrder, page, pageSize, userId); // ✅ Gunakan await
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RequestDrug()
        {
            var obatListViewModel = await _obatRepo.GetAllDataAsync("", "", "", 1, 5);
            ViewBag.Medicines = new SelectList(obatListViewModel.Obats, "Id", "DrugName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestObatViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obatListViewModel = await _obatRepo.GetAllDataAsync("", "", "", 1, 5);
                    ViewBag.Medicines = new SelectList(obatListViewModel.Obats, "Id", "DrugName");

                    var userid = _userManager.GetUserId(User);

                    await _requestRepo.CreateRequestAsync(model, userid);

                    TempData["SuccessMessage"] = "Request successfully!";

                    return RedirectToAction("Index", "Request");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while Adding the drug.";

                    return View(model);
                }
            }
            return View(model);
        }
    }
}
