using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyGiaoVu.Data;
using X.PagedList;
namespace QuanLyGiaoVu.Controllers
{
    public class ChamCongController : Controller
    {
        private readonly QlgvContext _context;

        public ChamCongController(QlgvContext context)
        {
            _context = context;
        }
        // GET: ChamCongController
        [HttpGet]
        public async Task<IActionResult> TraCuuChamCong(int? idlh = 0, int? idgv = 0, int page = 1, int pagesize = 5)
        {
            var tccc = _context.Thongtinchamcongs.Include(lp => lp.MalophocNavigation).Include(hv => hv.MagiaovienNavigation).AsQueryable();
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", idlh);
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen", idgv);
            if (idlh != 0 && idgv != 0)
            {
                tccc = tccc.Where(s => s.Malophoc == idlh && s.Magiaovien == idgv);
            }
            else
            {
                if(idlh!=0 && idgv == 0)
                {
                    tccc = tccc.Where(s => s.Malophoc == idlh && s.Magiaovien> 0);
                }
                if (idlh == 0 && idgv != 0)
                {
                    tccc = tccc.Where(s => s.Malophoc >0 && s.Magiaovien == idgv);
                }
                else
                {
                    tccc = tccc.Where(s => s.Malophoc > 0 && s.Magiaovien > 0);
                }
            }
            
            return View(await tccc.ToPagedListAsync(page, pagesize));
        }

        // GET: ChamCongController/Create
        [HttpGet]
        public IActionResult ThucHienChamCong()
        {
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View();
        }

        // POST: ChamCongController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThucHienChamCong([Bind("Sott,Magiaovien,Malophoc,Thoigianchamcong")] Thongtinchamcong chamcong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chamcong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TraCuuChamCong));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen", chamcong.Magiaovien);

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", chamcong.Malophoc);
            return View(chamcong);
        }

        // GET: ChamCongController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChamCongController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChamCongController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cc = await _context.Thongtinchamcongs
                .Include(d => d.MagiaovienNavigation)
                .Include(d => d.MalophocNavigation)
                .FirstOrDefaultAsync(m => m.Sott == id);
            if (cc == null)
            {
                return NotFound();
            }

            return View(cc);
        }

        // POST: GiaoVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cc = await _context.Thongtinchamcongs.FindAsync(id);
            if (cc != null)
            {
                _context.Thongtinchamcongs.Remove(cc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TraCuuChamCong));
        }
    }
}
