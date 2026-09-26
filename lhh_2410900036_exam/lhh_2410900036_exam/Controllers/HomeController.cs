using lhh_2410900036_exam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lhh_2410900036_exam.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult lhhAbout()
        {
            lhhAbout about = new lhhAbout
            {
                Name = "Lê Hoàng Huy",
                MSV = "2410900036",
                Class = "K24CNT2",
                Email = "hoangvb1802@gmail.com",
                Phone = "0866967226"
            };
            return View(about);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
