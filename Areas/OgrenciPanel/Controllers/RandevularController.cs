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

namespace BatikanSon.Areas.OgrenciPanel.Controllers
{
    [Area("OgrenciPanel")]
    [Authorize(Roles = "Ogrenci")]
    public class RandevularController : Controller
    {
        private readonly LangDB _context;
        private readonly UserManager<Uye> _userManager;

        public RandevularController(LangDB context, UserManager<Uye> userManager)
        {
            _userManager= userManager;
            _context = context;
        }

        // GET: OgrenciPanel/Randevular
        public async Task<IActionResult> Index()
        {
            
            var langDB = _context.Randevu_VMs.Where(x => x.OgrenciID == int.Parse(_userManager.GetUserId(User)));
            return View(await langDB.ToListAsync());
        }

        // GET: OgrenciPanel/Randevular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Ogrenci)
                .Include(r => r.Ogretmen)
                .FirstOrDefaultAsync(m => m.RandevuID == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: OgrenciPanel/Randevular/Create
        public IActionResult Create(int? id=1)
        {
            ViewBag.Diller = new SelectList(_context.Diller, "DilID", "DilAdi", id.Value.ToString());

            var liste = _context.RandevuTanimlari.Include("Dil").Include("Gun").Include("Saat").Include("Mekan").Where(x => x.DilID == id && x.Onay != true).ToList();

            
            List<Randevu_Detay> rd = new List<Randevu_Detay>();
            foreach(var item in liste)
            {
                
                
                rd.Add(new Randevu_Detay() { ID = item.ID, Detay = item.Dil.DilAdi + " " + item.Gun.GunAdi + " " + item.Saat.SaatAdi + " " + item.Mekan.MekanAdi });
            }
            ViewData["RTID"] = new SelectList(rd, "ID", "Detay");

            return View();
        }

        // POST: OgrenciPanel/Randevular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int RTID)
        {
            if (ModelState.IsValid)
            {
                _context.Randevular.Add(new Randevu
                {
                    RTID = RTID,
                    OgrenciID = int.Parse(_userManager.GetUserId(User)),
                    
                    OgretmenID = _context.RandevuTanimlari.Where(x => x.ID == RTID).Select(x => x.OgretmenID).Single()
                });
                
                _context.Randevu_VMs.Add(new Randevu_VM()
                {
                    OgrenciID = int.Parse(_userManager.GetUserId(User)),
                    
                    OgrenciAdi = _context.Uyeler.Where(x => x.Id == int.Parse(_userManager.GetUserId(User))).Select(x => x.Ad).Single(),
                    OgrenciSoyadi = _context.Uyeler.Where(x => x.Id == int.Parse(_userManager.GetUserId(User))).Select(x => x.Soyad).Single(),
                    OgretmenID = _context.RandevuTanimlari.Where(x => x.ID == RTID).Select(x => x.OgretmenID).Single(),
                    OgretmenAdi = _context.RandevuTanimlari.Include("OgretmenRT").Where(x => x.ID == RTID).Select(x => x.OgretmenRT.Ad).Single(),
                    OgretmenSoyadi = _context.RandevuTanimlari.Include("OgretmenRT").Where(x => x.ID == RTID).Select(x => x.OgretmenRT.Soyad).Single(),
                    //27.01 Bitiş
                    DilAdi = _context.RandevuTanimlari.Include("Dil").Where(x => x.ID == RTID).Select(x => x.Dil.DilAdi).Single(),
                    GunAdi = _context.RandevuTanimlari.Include("Gun").Where(x => x.ID == RTID).Select(x => x.Gun.GunAdi).Single(),
                    SaatAdi = _context.RandevuTanimlari.Include("Saat").Where(x => x.ID == RTID).Select(x => x.Saat.SaatAdi).Single(),
                    MekanAdi = _context.RandevuTanimlari.Include("Mekan").Where(x => x.ID == RTID).Select(x => x.Mekan.MekanAdi).Single()
                }); 
                await _context.SaveChangesAsync();
                
                var randevuTanimi = _context.RandevuTanimlari.Find(RTID);
                randevuTanimi.Onay = true;
                _context.Entry<RandevuTanimi>(randevuTanimi).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View(/*randevu*/);
        }

        // GET: OgrenciPanel/Randevular/Edit/5
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

        // POST: OgrenciPanel/Randevular/Edit/5
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

        // GET: OgrenciPanel/Randevular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Ogrenci)
                .Include(r => r.Ogretmen)
                .FirstOrDefaultAsync(m => m.RandevuID == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: OgrenciPanel/Randevular/Delete/5
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
          return _context.Randevular.Any(e => e.RandevuID == id);
        }
    }
}
