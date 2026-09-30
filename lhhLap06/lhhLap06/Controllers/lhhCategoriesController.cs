
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lhhLap06.Models;
using lhhLap06.Entities;

public class lhhCategoriesController : Controller
{
    private readonly AppDbContext _context;

    public lhhCategoriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: LHHCATEGORYS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Categories.ToListAsync());
    }

    // GET: LHHCATEGORYS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lhhcategory = await _context.Categories
            .FirstOrDefaultAsync(m => m.categoryId == id);
        if (lhhcategory == null)
        {
            return NotFound();
        }

        return View(lhhcategory);
    }

    // GET: LHHCATEGORYS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LHHCATEGORYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("categoryId,categoryName,status,CreatedDate")] lhhCategory lhhcategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lhhcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(lhhcategory);
    }

    // GET: LHHCATEGORYS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lhhcategory = await _context.Categories.FindAsync(id);
        if (lhhcategory == null)
        {
            return NotFound();
        }
        return View(lhhcategory);
    }

    // POST: LHHCATEGORYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("categoryId,categoryName,status,CreatedDate")] lhhCategory lhhcategory)
    {
        if (id != lhhcategory.categoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(lhhcategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LhhCategoryExists(lhhcategory.categoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(lhhcategory);
    }

    // GET: LHHCATEGORYS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lhhcategory = await _context.Categories
            .FirstOrDefaultAsync(m => m.categoryId == id);
        if (lhhcategory == null)
        {
            return NotFound();
        }

        return View(lhhcategory);
    }

    // POST: LHHCATEGORYS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var lhhcategory = await _context.Categories.FindAsync(id);
        if (lhhcategory != null)
        {
            _context.Categories.Remove(lhhcategory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LhhCategoryExists(int? id)
    {
        return _context.Categories.Any(e => e.categoryId == id);
    }
}
