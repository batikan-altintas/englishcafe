using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BatikanSon.DAL;
using BatikanSon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BatikanSon.Areas.OgretmenPanel.Controllers
{
    [Area("OgretmenPanel")]
    [Authorize(Roles = "Ogretmen")]
    public class RandevuTanimlaController : Controller
    {
        private readonly LangDB _context;
        private readonly UserManager<Uye> _userManager;

        public RandevuTanimlaController(LangDB context, UserManager<Uye> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: OgretmenPanel/RandevuTanimla
        public async Task<IActionResult> Index()
        {
            var langDB = _context.RandevuTanimlari.Include(r => r.Dil).Include(r => r.Gun).Include(r => r.Mekan).Include(r => r.OgretmenRT).Include(r => r.Saat).Where(x => x.OgretmenID == int.Parse(_userManager.GetUserId(User)) && x.Onay != true);
            return View(await langDB.ToListAsync());
        }

        // GET: OgretmenPanel/RandevuTanimla/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RandevuTanimlari == null)
            {
                return NotFound();
            }

            var randevuTanimi = await _context.RandevuTanimlari
                .Include(r => r.Dil)
                .Include(r => r.Gun)
                .Include(r => r.Mekan)
                
                .Include(r => r.OgretmenRT)
                .Include(r => r.Saat)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (randevuTanimi == null)
            {
                return NotFound();
            }

            return View(randevuTanimi);
        }

        // GET: OgretmenPanel/RandevuTanimla/Create
        public IActionResult Create()
        {
            ViewData["DilID"] = new SelectList(_context.Diller, "DilID", "DilAdi");
            ViewData["GunID"] = new SelectList(_context.Gunler, "GunID", "GunAdi");
            ViewData["MekanID"] = new SelectList(_context.Mekanlar, "MekanID", "MekanAdi");
            
            ViewData["SaatID"] = new SelectList(_context.Saatler, "SaatID", "SaatAdi");
            return View();
        }

        // POST: OgretmenPanel/RandevuTanimla/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OgretmenID,DilID,GunID,SaatID,MekanID,Onay")] RandevuTanimi randevuTanimi)
        {
            if (ModelState.IsValid)
            {
                randevuTanimi.OgretmenID = int.Parse(_userManager.GetUserId(User));
                _context.Add(randevuTanimi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DilID"] = new SelectList(_context.Diller, "DilID", "DilID", randevuTanimi.DilID);
            ViewData["GunID"] = new SelectList(_context.Gunler, "GunID", "GunID", randevuTanimi.GunID);
            ViewData["MekanID"] = new SelectList(_context.Mekanlar, "MekanID", "MekanID", randevuTanimi.MekanID);
            
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id", randevuTanimi.OgretmenID);
            ViewData["SaatID"] = new SelectList(_context.Saatler, "SaatID", "SaatID", randevuTanimi.SaatID);
            return View(randevuTanimi);
        }

        // GET: OgretmenPanel/RandevuTanimla/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RandevuTanimlari == null)
            {
                return NotFound();
            }

            var randevuTanimi = await _context.RandevuTanimlari.FindAsync(id);
            if (randevuTanimi == null)
            {
                return NotFound();
            }
            ViewData["DilID"] = new SelectList(_context.Diller, "DilID", "DilAdi", randevuTanimi.DilID);
            ViewData["GunID"] = new SelectList(_context.Gunler, "GunID", "GunAdi", randevuTanimi.GunID);
            ViewData["MekanID"] = new SelectList(_context.Mekanlar, "MekanID", "MekanAdi", randevuTanimi.MekanID);
           
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id", randevuTanimi.OgretmenID);
            ViewData["SaatID"] = new SelectList(_context.Saatler, "SaatID", "SaatAdi", randevuTanimi.SaatID);
            return View(randevuTanimi);
        }

        // POST: OgretmenPanel/RandevuTanimla/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OgretmenID,DilID,GunID,SaatID,MekanID,Onay")] RandevuTanimi randevuTanimi)
        {
            if (id != randevuTanimi.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevuTanimi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuTanimiExists(randevuTanimi.ID))
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
            ViewData["DilID"] = new SelectList(_context.Diller, "DilID", "DilID", randevuTanimi.DilID);
            ViewData["GunID"] = new SelectList(_context.Gunler, "GunID", "GunID", randevuTanimi.GunID);
            ViewData["MekanID"] = new SelectList(_context.Mekanlar, "MekanID", "MekanID", randevuTanimi.MekanID);
            
            ViewData["OgretmenID"] = new SelectList(_context.Uyeler, "Id", "Id", randevuTanimi.OgretmenID);
            ViewData["SaatID"] = new SelectList(_context.Saatler, "SaatID", "SaatID", randevuTanimi.SaatID);
            return View(randevuTanimi);
        }

        // GET: OgretmenPanel/RandevuTanimla/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RandevuTanimlari == null)
            {
                return NotFound();
            }

            var randevuTanimi = await _context.RandevuTanimlari
                .Include(r => r.Dil)
                .Include(r => r.Gun)
                .Include(r => r.Mekan)
               
                .Include(r => r.OgretmenRT)
                .Include(r => r.Saat)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (randevuTanimi == null)
            {
                return NotFound();
            }

            return View(randevuTanimi);
        }

        // POST: OgretmenPanel/RandevuTanimla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RandevuTanimlari == null)
            {
                return Problem("Entity set 'LangDB.RandevuTanimlari'  is null.");
            }
            var randevuTanimi = await _context.RandevuTanimlari.FindAsync(id);
            if (randevuTanimi != null)
            {
                _context.RandevuTanimlari.Remove(randevuTanimi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuTanimiExists(int id)
        {
          return _context.RandevuTanimlari.Any(e => e.ID == id);
        }

        
        
        public IActionResult AktifRandevular()
        {

            var langDB = _context.Randevu_VMs.Where(x => x.OgretmenID == int.Parse(_userManager.GetUserId(User)));
            return View(langDB.ToList());
        }
    }
}
