using lhhLesson3.Models;
using Microsoft.AspNetCore.Mvc;

namespace lhhLesson3.Controllers
{
    public class lhhProductController : Controller
    {   
        // tao mock data
        private readonly List<Models.lhhProduct> _products = new List<Models.lhhProduct>
        {
            new lhhProduct
    {
        lhhProductID = "GD001",
        lhhProductName = "RX-78-2 Gundam",
        lhhPrice = 850000,
        lhhYearRelease = "1979"
    },
    new lhhProduct
    {
        lhhProductID = "GD002",
        lhhProductName = "MS-06S Zaku II",
        lhhPrice = 750000,
        lhhYearRelease = "1979"
    },
    new lhhProduct
    {
        lhhProductID = "GD003",
        lhhProductName = "RX-93 Nu Gundam",
        lhhPrice = 1250000,
        lhhYearRelease = "1988"
    },
    new lhhProduct
    {
        lhhProductID = "GD004",
        lhhProductName = "MSN-04 Sazabi",
        lhhPrice = 1350000,
        lhhYearRelease = "1988"
    },
    new lhhProduct
    {
        lhhProductID = "GD005",
        lhhProductName = "GAT-X105 Strike Gundam",
        lhhPrice = 950000,
        lhhYearRelease = "2002"
    },
    new lhhProduct
    {
        lhhProductID = "GD006",
        lhhProductName = "ZGMF-X10A Freedom Gundam",
        lhhPrice = 1100000,
        lhhYearRelease = "2002"
    },
    new lhhProduct
    {
        lhhProductID = "GD007",
        lhhProductName = "ZGMF-X20A Strike Freedom Gundam",
        lhhPrice = 1450000,
        lhhYearRelease = "2004"
    },
    new lhhProduct
    {
        lhhProductID = "GD008",
        lhhProductName = "GN-001 Gundam Exia",
        lhhPrice = 1000000,
        lhhYearRelease = "2007"
    },
    new lhhProduct
    {
        lhhProductID = "GD009",
        lhhProductName = "RX-0 Unicorn Gundam",
        lhhPrice = 1300000,
        lhhYearRelease = "2010"
    },
    new lhhProduct
    {
        lhhProductID = "GD010",
        lhhProductName = "RX-0 Unicorn Gundam 02 Banshee",
        lhhPrice = 1400000,
        lhhYearRelease = "2012"
    }
        };
        public IActionResult Index()
        {
            return Json(_products);
        }
        // get danh sach san pham
        public IActionResult lhhGetAllProducts()
        {
            ViewData["Products"] = _products;
            return View();
        }
        public IActionResult lhhGetListProduct()
        {
            return View(_products);
        }
    }
}
