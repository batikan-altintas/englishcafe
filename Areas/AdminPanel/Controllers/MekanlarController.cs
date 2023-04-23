using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BatikanSon.DAL;
using BatikanSon.Models;
using Microsoft.AspNetCore.Authorization;

namespace BatikanSon.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class MekanlarController : Controller
    {
        private readonly LangDB _context;

        public MekanlarController(LangDB context)
        {
            _context = context;
        }

        // GET: AdminPanel/Mekanlar
        public async Task<IActionResult> Index()
        {
              return View(await _context.Mekanlar.ToListAsync());
        }

        // GET: AdminPanel/Mekanlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mekanlar == null)
            {
                return NotFound();
            }

            var mekan = await _context.Mekanlar
                .FirstOrDefaultAsync(m => m.MekanID == id);
            if (mekan == null)
            {
                return NotFound();
            }

            return View(mekan);
        }

        // GET: AdminPanel/Mekanlar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Mekanlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MekanID,MekanAdi")] Mekan mekan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mekan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mekan);
        }

        // GET: AdminPanel/Mekanlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mekanlar == null)
            {
                return NotFound();
            }

            var mekan = await _context.Mekanlar.FindAsync(id);
            if (mekan == null)
            {
                return NotFound();
            }
            return View(mekan);
        }

        // POST: AdminPanel/Mekanlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MekanID,MekanAdi")] Mekan mekan)
        {
            if (id != mekan.MekanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mekan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MekanExists(mekan.MekanID))
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
            return View(mekan);
        }

        // GET: AdminPanel/Mekanlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mekanlar == null)
            {
                return NotFound();
            }

            var mekan = await _context.Mekanlar
                .FirstOrDefaultAsync(m => m.MekanID == id);
            if (mekan == null)
            {
                return NotFound();
            }

            return View(mekan);
        }

        // POST: AdminPanel/Mekanlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mekanlar == null)
            {
                return Problem("Entity set 'LangDB.Mekanlar'  is null.");
            }
            var mekan = await _context.Mekanlar.FindAsync(id);
            if (mekan != null)
            {
                _context.Mekanlar.Remove(mekan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MekanExists(int id)
        {
          return _context.Mekanlar.Any(e => e.MekanID == id);
        }
    }
}
