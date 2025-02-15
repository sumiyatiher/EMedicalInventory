using Microsoft.AspNetCore.Mvc;

namespace EMedicalInventory.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
