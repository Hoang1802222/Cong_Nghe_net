using Microsoft.AspNetCore.Mvc;
using lhhLesson06ne.Models;
namespace lhhLesson06ne.ViewComponents

{
    public class CategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int? n)
        {
            List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
                new Category { Id = 2, Name = "Books", Description = "Various genres of books" },
                new Category {Id = 3, Name = "Clothing", Description = "Apparel and fashion items" },
                new Category {Id = 4, Name = "Home & Kitchen", Description = "Household and kitchen items" },
                new Category {Id = 5, Name = "Sports & Outdoors", Description = "Sports equipment and outdoor gear" },
            };
            n = n ?? 0;
            var search = categories.Where(x => x.Id > n).ToList();
            return View(search);
        }
    }
}
