using Microsoft.AspNetCore.Mvc;
using lhhLesson08.Models;

namespace lhhLesson08.Controllers
{
    public class lhhMemberController : Controller
    {
        // tao moc data
        private static List<lhhMember> _members = new List<lhhMember>
    {
        new lhhMember
        {
            lhhMemberID = Guid.NewGuid().ToString(),
            lhhMemberName = "John Doe",
            lhhMemberEmail = "john.doe@example.com",
            lhhMemberPassword = "password123",
            lhhMemberPhone = "123-456-7890"
        },

        new lhhMember
        {
            lhhMemberID = Guid.NewGuid().ToString(),
            lhhMemberName = "Nguyen Van An",
            lhhMemberEmail = "nguyenvanan@gmail.com",
            lhhMemberPassword = "123456",
            lhhMemberPhone = "0901234567"
        },

        new lhhMember
        {
            lhhMemberID = Guid.NewGuid().ToString(),
            lhhMemberName = "Tran Thi Binh",
            lhhMemberEmail = "tranthibinh@gmail.com",
            lhhMemberPassword = "123456",
            lhhMemberPhone = "0912345678"
        },

        new lhhMember
        {
            lhhMemberID = Guid.NewGuid().ToString(),
            lhhMemberName = "Le Van Cuong",
            lhhMemberEmail = "levancuong@gmail.com",
            lhhMemberPassword = "123456",
            lhhMemberPhone = "0987654321"
        },

        new lhhMember
        {
            lhhMemberID = Guid.NewGuid().ToString(),
            lhhMemberName = "Pham Thi Dung",
            lhhMemberEmail = "phamthidung@gmail.com",
            lhhMemberPassword = "123456",
            lhhMemberPhone = "0978123456"
        }
    };

        public IActionResult Index()
        {
            return View(_members);
        }
        [HttpGet]
        public IActionResult lhhCreate()
        {
            var newMember = new lhhMember();
            return View(newMember);
        }
        [HttpPost]
        public IActionResult lhhCreate(lhhMember member)
        {
            member.lhhMemberID = Guid.NewGuid().ToString();
            _members.Add(member);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult lhhEdit(string id)
        {
            var member = _members.FirstOrDefault(m => m.lhhMemberID == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [HttpPost]
        public IActionResult lhhEdit(string id, lhhMember member)
        {
            //r member = _members.FirstOrDefault(m => m.lhhMemberID == id);
            for (int i = 0; i < _members.Count; i++)
            {
                if (_members[i].lhhMemberID == id)
                {
                    _members[i].lhhMemberName = member.lhhMemberName;
                    _members[i].lhhMemberEmail = member.lhhMemberEmail;
                    _members[i].lhhMemberPassword = member.lhhMemberPassword;
                    _members[i].lhhMemberPhone = member.lhhMemberPhone;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult lhhDetails(string id)
        {
            var member = _members.FirstOrDefault(m => m.lhhMemberID == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [HttpGet]
        public IActionResult lhhDelete(string id)
        {
            var member = _members.FirstOrDefault(m => m.lhhMemberID == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [HttpPost]
        
        public IActionResult lhhDeleteConfirmed(string id)
        {
            var member = _members.FirstOrDefault(m => m.lhhMemberID == id);
            if (member == null)
            {
                return NotFound();
            }
            _members.Remove(member);
            return RedirectToAction("Index");
        }
    }
}

