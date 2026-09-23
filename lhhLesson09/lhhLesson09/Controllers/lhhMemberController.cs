using lhhLesson09.Models.DataviewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lhhLesson09.Controllers
{
    public class lhhMemberController : Controller
    {
        // Khởi tạo danh sách theo lhhMemberRegister (DataviewModel)
        private static List<lhhMemberRegister> _lhhMembers = new List<lhhMemberRegister>
        {
            new lhhMemberRegister { lhhMemberId = 1, lhhMemberName = "member1", lhhMemberPassword = "password1", lhhMemberEmail = "member1@example.com", lhhMemberPhone = "0987654321", lhhFullname = "Thành viên 1", lhhBirtday = new DateTime(2000, 1, 1) },
            new lhhMemberRegister { lhhMemberId = 2, lhhMemberName = "member2", lhhMemberPassword = "password2", lhhMemberEmail = "member2@example.com", lhhMemberPhone = "0987654322", lhhFullname = "Thành viên 2", lhhBirtday = new DateTime(2001, 2, 2) },
            new lhhMemberRegister { lhhMemberId = 3, lhhMemberName = "member3", lhhMemberPassword = "password3", lhhMemberEmail = "member3@example.com", lhhMemberPhone = "0987654323", lhhFullname = "Thành viên 3", lhhBirtday = new DateTime(2002, 3, 3) },
            new lhhMemberRegister { lhhMemberId = 4, lhhMemberName = "member4", lhhMemberPassword = "password4", lhhMemberEmail = "member4@example.com", lhhMemberPhone = "0987654324", lhhFullname = "Thành viên 4", lhhBirtday = new DateTime(2003, 4, 4) },
            new lhhMemberRegister { lhhMemberId = 5, lhhMemberName = "member5", lhhMemberPassword = "password5", lhhMemberEmail = "member5@example.com", lhhMemberPhone = "0987654325", lhhFullname = "Thành viên 5", lhhBirtday = new DateTime(2004, 5, 5) },
            new lhhMemberRegister { lhhMemberId = 6, lhhMemberName = "member6", lhhMemberPassword = "password6", lhhMemberEmail = "member6@example.com", lhhMemberPhone = "0987654326", lhhFullname = "Thành viên 6", lhhBirtday = new DateTime(2005, 6, 6) }
        };

        // GET: lhhMemberController
        public ActionResult Index()
        {
            // Đã truyền _lhhMembers vào View
            return View(_lhhMembers);
        }

        // GET: lhhMemberController/Details/5
        public ActionResult Details(int id)
        {
            var member = _lhhMembers.FirstOrDefault(m => m.lhhMemberId == id);
            if (member == null) return NotFound();
            return View(member);
        }

        // GET: lhhMemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: lhhMemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(lhhMemberRegister model)
        {
            if (ModelState.IsValid)
            {
                model.lhhMemberId = _lhhMembers.Any() ? _lhhMembers.Max(m => m.lhhMemberId) + 1 : 1;
                _lhhMembers.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: lhhMemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _lhhMembers.FirstOrDefault(m => m.lhhMemberId == id);
            if (member == null) return NotFound();
            return View(member);
        }

        // POST: lhhMemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, lhhMemberRegister model)
        {
            if (ModelState.IsValid)
            {
                var member = _lhhMembers.FirstOrDefault(m => m.lhhMemberId == id);
                if (member == null) return NotFound();

                member.lhhMemberName = model.lhhMemberName;
                member.lhhMemberPassword = model.lhhMemberPassword;
                member.lhhMemberEmail = model.lhhMemberEmail;
                member.lhhMemberPhone = model.lhhMemberPhone;
                member.lhhFullname = model.lhhFullname;
                member.lhhBirtday = model.lhhBirtday;

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: lhhMemberController/Delete/5
        public ActionResult Delete(int id)
        {
            var member = _lhhMembers.FirstOrDefault(m => m.lhhMemberId == id);
            if (member == null) return NotFound();
            return View(member);
        }

        // POST: lhhMemberController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var member = _lhhMembers.FirstOrDefault(m => m.lhhMemberId == id);
            if (member != null)
            {
                _lhhMembers.Remove(member);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}