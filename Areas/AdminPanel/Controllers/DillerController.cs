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
    public class DillerController : Controller
    {
        private readonly LangDB _context;

        public DillerController(LangDB context)
        {
            _context = context;
        }

        // GET: AdminPanel/Dil
        public async Task<IActionResult> Index()
        {
              return View(await _context.Diller.ToListAsync());
        }

        // GET: AdminPanel/Dil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diller == null)
            {
                return NotFound();
            }

            var dil = await _context.Diller
                .FirstOrDefaultAsync(m => m.DilID == id);
            if (dil == null)
            {
                return NotFound();
            }

            return View(dil);
        }

        // GET: AdminPanel/Dil/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Dil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DilID,DilAdi")] Dil dil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dil);
        }

        // GET: AdminPanel/Dil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diller == null)
            {
                return NotFound();
            }

            var dil = await _context.Diller.FindAsync(id);
            if (dil == null)
            {
                return NotFound();
            }
            return View(dil);
        }

        // POST: AdminPanel/Dil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DilID,DilAdi")] Dil dil)
        {
            if (id != dil.DilID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DilExists(dil.DilID))
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
            return View(dil);
        }

        // GET: AdminPanel/Dil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diller == null)
            {
                return NotFound();
            }

            var dil = await _context.Diller
                .FirstOrDefaultAsync(m => m.DilID == id);
            if (dil == null)
            {
                return NotFound();
            }

            return View(dil);
        }

        // POST: AdminPanel/Dil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diller == null)
            {
                return Problem("Entity set 'LangDB.Diller'  is null.");
            }
            var dil = await _context.Diller.FindAsync(id);
            if (dil != null)
            {
                _context.Diller.Remove(dil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DilExists(int id)
        {
          return _context.Diller.Any(e => e.DilID == id);
        }
    }
}
