
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lhh_2410900036_exam.Models;

public class LhhEmployesController : Controller
{
    private readonly LhhReviewLesson10Context _context;

    public LhhEmployesController(LhhReviewLesson10Context context)
    {
        _context = context;
    }

    // GET: LHHEMPLOYES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.LhhEmployes.ToListAsync());
    }

    // GET: LHHEMPLOYES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lhhemploye = await _context.LhhEmployes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lhhemploye == null)
        {
            return NotFound();
        }

        return View(lhhemploye);
    }

    // GET: LHHEMPLOYES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LHHEMPLOYES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LhhName,LhhGender,LhhBirthDay,LhhEmail,LhhPhone,LhhActive")] LhhEmploye lhhemploye)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lhhemploye);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(lhhemploye);
    }

    // GET: LHHEMPLOYES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lhhemploye = await _context.LhhEmployes.FindAsync(id);
        if (lhhemploye == null)
        {
            return NotFound();
        }
        return View(lhhemploye);
    }

    // POST: LHHEMPLOYES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,LhhName,LhhGender,LhhBirthDay,LhhEmail,LhhPhone,LhhActive")] LhhEmploye lhhemploye)
    {
        if (id != lhhemploye.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(lhhemploye);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LhhEmployeExists(lhhemploye.Id))
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
        return View(lhhemploye);
    }

    // GET: LHHEMPLOYES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var lhhemploye = await _context.LhhEmployes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lhhemploye == null)
        {
            return NotFound();
        }

        return View(lhhemploye);
    }

    // POST: LHHEMPLOYES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var lhhemploye = await _context.LhhEmployes.FindAsync(id);
        if (lhhemploye != null)
        {
            _context.LhhEmployes.Remove(lhhemploye);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LhhEmployeExists(int? id)
    {
        return _context.LhhEmployes.Any(e => e.Id == id);
    }
}
