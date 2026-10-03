using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace lhhLesson14.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CategoriesController : Controller
    {
        // Khởi tạo danh sách tĩnh
        private static List<Models.Category> categories = new List<Models.Category>
        {
            new Models.Category { Id = 1, Name = "Category 1" },
            new Models.Category { Id = 2, Name = "Category 2" },
            new Models.Category { Id = 3, Name = "Category 3" }
        };

        // GET: Categories
        public ActionResult Index()
        {
            return View(categories);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Category category)
        {
            try
            {
                // Tự động tăng Id
                category.Id = categories.Any() ? categories.Max(c => c.Id) + 1 : 1;
                categories.Add(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            // SỬA LỖI: Bắt buộc truyền category vào View
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Models.Category category)
        {
            try
            {
                var existingCategory = categories.FirstOrDefault(c => c.Id == id);
                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            // SỬA LỖI: Bắt buộc truyền category vào View
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var category = categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    categories.Remove(category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}