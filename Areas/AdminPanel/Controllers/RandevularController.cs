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
    public class RandevularController : Controller
    {
        private readonly LangDB _context;

        public RandevularController(LangDB context)
        {
            _context = context;
        }

        // GET: AdminPanel/Randevular
        public async Task<IActionResult> Index()
        {
            //var langDB = _context.Randevular.Include(r => r.Ogrenci).Include(r => r.Ogretmen);
            var langDB = _context.Randevular.Include(r => r.Ogrenci).Include(r => r.Ogretmen).Include(r => r.RandevuTanimi.Dil).Include(r => r.RandevuTanimi.Gun).Include(r => r.RandevuTanimi.Saat).Include(r => r.RandevuTanimi.Mekan);
            return View(await langDB.ToListAsync());
        }

        // GET: AdminPanel/Randevular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Ogrenci)
                .Include(r => r.Ogretmen).Include(r => r.RandevuTanimi.Dil).Include(r => r.RandevuTanimi.Gun).Include(r => r.RandevuTanimi.Saat).Include(r => r.RandevuTanimi.Mekan)
                .FirstOrDefaultAsync(m => m.RandevuID == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: AdminPanel/Randevular/Create
        public IActionResult Create()
        {
            ViewData["OgrenciID"] = new SelectList(_context.Uyeler, "Id", "Id");
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id");
            return View();
        }

        // POST: AdminPanel/Randevular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandevuID,OgretmenID,OgrenciID,RTID,GerceklestiMi")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgrenciID"] = new SelectList(_context.Uyeler, "Id", "Id", randevu.OgrenciID);
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id", randevu.OgretmenID);
            return View(randevu);
        }

        // GET: AdminPanel/Randevular/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["OgrenciID"] = new SelectList(_context.Uyeler, "Id", "Id", randevu.OgrenciID);
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id", randevu.OgretmenID);
            return View(randevu);
        }

        // POST: AdminPanel/Randevular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RandevuID,OgretmenID,OgrenciID,RTID,GerceklestiMi")] Randevu randevu)
        {
            if (id != randevu.RandevuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.RandevuID))
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
            ViewData["OgrenciID"] = new SelectList(_context.Uyeler, "Id", "Id", randevu.OgrenciID);
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id", randevu.OgretmenID);
            return View(randevu);
        }

        // GET: AdminPanel/Randevular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Ogrenci)
                .Include(r => r.Ogretmen).Include(r => r.RandevuTanimi.Dil).Include(r => r.RandevuTanimi.Gun).Include(r => r.RandevuTanimi.Saat).Include(r => r.RandevuTanimi.Mekan)
                .FirstOrDefaultAsync(m => m.RandevuID == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: AdminPanel/Randevular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevular == null)
            {
                return Problem("Entity set 'LangDB.Randevular'  is null.");
            }
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevular?.Any(e => e.RandevuID == id)).GetValueOrDefault();
        }
    }
}
