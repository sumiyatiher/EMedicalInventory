using EMedicalInventory.Data;
using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMedicalInventory.Controllers
{
    public class ObatController : Controller
    {
        private readonly MedicalInventoryDBContext _dbContext;
        private readonly UserManager<Users> _userManager;

        public ObatController(MedicalInventoryDBContext context, UserManager<Users> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var obatList = _dbContext.Obat.ToList();
            return View(obatList);
        }

        [HttpGet]
        public IActionResult AddDrug()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ObatViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Simpan data jika valid
                var userId = _userManager.GetUserId(User);

                var obat = new Obat
                {
                    DrugName = model.DrugName,
                    Stock = model.Stock,
                    Price = model.Price,
                    ExpiredDate = model.ExpiredDate,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.Now

                };

                _dbContext.Obat.Add(obat);
                _dbContext.SaveChanges();

                var VMObat = new ObatViewModel
                {
                    Id = obat.Id,
                    DrugName = obat.DrugName,
                    Stock = obat.Stock,
                    Price = obat.Price,
                    ExpiredDate = obat.ExpiredDate
                };
                return RedirectToAction("Index", "Obat");

                

            }

            return View(model);
        }
    }
}
