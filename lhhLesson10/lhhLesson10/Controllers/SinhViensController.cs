using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lhhLesson10.Models;

namespace lhhLesson10.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly LhhLesson10Context _context;

        public SinhViensController(LhhLesson10Context context)
        {
            _context = context;
        }

        // GET: SinhViens
        public async Task<IActionResult> Index()
        {
            return View(await _context.SinhViens.ToListAsync());
        }

        // GET: SinhViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .FirstOrDefaultAsync(m => m.MaSv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSv,HoTen,NgaySinh,GioiTinh,Email")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sinhVien);
        }

        // GET: SinhViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSv,HoTen,NgaySinh,GioiTinh,Email")] SinhVien sinhVien)
        {
            if (id != sinhVien.MaSv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSv))
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
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .FirstOrDefaultAsync(m => m.MaSv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienExists(int id)
        {
            return _context.SinhViens.Any(e => e.MaSv == id);
        }
    }
}
