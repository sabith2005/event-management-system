using Microsoft.AspNetCore.Mvc;

namespace KMCEventAdmin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}