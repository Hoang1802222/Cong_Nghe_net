using Microsoft.AspNetCore.Mvc;

namespace lhhLesson14.Areas.Admins.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admins")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
