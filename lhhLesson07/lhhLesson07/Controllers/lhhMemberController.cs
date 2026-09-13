using Microsoft.AspNetCore.Mvc;
using lhhLesson07.Models.DataModel;
namespace lhhLesson07.Controllers
{
    public class lhhMemberController : Controller
    {
        protected static List<lhhMember> _members = new List<lhhMember>
        {
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "hoang.lh", lhhPassword = "Password123!", lhhEmail = "hoang.lh@example.com", lhhFullName = "Lê Huy Hoàng" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "minh.nv", lhhPassword = "Password123!", lhhEmail = "minh.nv@example.com", lhhFullName = "Nguyễn Văn Minh" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "trang.tt", lhhPassword = "Password123!", lhhEmail = "trang.tt@example.com", lhhFullName = "Trần Thị Thu Trang" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "dung.pv", lhhPassword = "Password123!", lhhEmail = "dung.pv@example.com", lhhFullName = "Phạm Văn Dũng" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "lan.ht", lhhPassword = "Password123!", lhhEmail = "lan.ht@example.com", lhhFullName = "Hoàng Thị Lan" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "tuan.an", lhhPassword = "Password123!", lhhEmail = "tuan.an@example.com", lhhFullName = "Anh Tuấn Nguyễn" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "linh.dt", lhhPassword = "Password123!", lhhEmail = "linh.dt@example.com", lhhFullName = "Đỗ Thùy Linh" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "khang.vo", lhhPassword = "Password123!", lhhEmail = "khang.vo@example.com", lhhFullName = "Võ Văn Khang" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "ha.tm", lhhPassword = "Password123!", lhhEmail = "ha.tm@example.com", lhhFullName = "Trịnh Mai Hà" },
            new() { lhhMemberId = Guid.NewGuid().ToString(), lhhUserName = "nam.dh", lhhPassword = "Password123!", lhhEmail = "nam.dh@example.com", lhhFullName = "Đặng Hải Nam" }
        };
        public IActionResult Index()
        {

            return View(_members);
        }
        public IActionResult GetMember()
        {
            var member = new lhhMember
            {
                lhhMemberId = Guid.NewGuid().ToString(),
                lhhUserName = "Hoang",
                lhhPassword = "password123",
                lhhEmail = "hoangvb1802@gmail.com",
                lhhFullName = "Le Huy Hoang"
            };
            ViewBag.Member = member;
            return View(member);
        }
        public IActionResult GetMembers()
        {
            ViewBag.Members = _members;
            return View();
        }
        // dua du lieu dang list ra  view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(lhhMember member)
        {
            if (ModelState.IsValid)
            {
                member.lhhMemberId = Guid.NewGuid().ToString();
                _members.Add(member);
                return RedirectToAction("Index");
            }
            return View(member);
        }

    }
}