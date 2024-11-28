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
    public class LoiQuyTrinhController : Controller
    {
        private readonly QlgvContext _context;
        public LoiQuyTrinhController(QlgvContext context)
        {
            _context = context;
        }
        // GET: LoiQuyTrinhController
        public async Task<IActionResult> Index(int page = 1, int pagesize = 5)
        {
            var qlgvContext = _context.Loiquytrinhs.Include(d => d.MagiaovienNavigation).Include(d => d.MalophocNavigation);
            return View(await qlgvContext.ToPagedListAsync(page, pagesize));
        }

        // GET: LoiQuyTrinhController/Create
        public IActionResult Create()
        {
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View();
        }

        // POST: LoiQuyTrinhController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maloi,Malophoc,Magiaovien,Tenloi,Motaloi,Ngayvipham")] Loiquytrinh loiquytrinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loiquytrinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                "Magiaovien", "HoTen", loiquytrinh.Magiaovien);

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", loiquytrinh.Malophoc);
            return View(loiquytrinh);
        }

        // GET: LoiQuyTrinhController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loiquytrinh = await _context.Loiquytrinhs.FindAsync(id);
            if (loiquytrinh == null)
            {
                return NotFound();
            }
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View(loiquytrinh);
        }

        // POST: LoiQuyTrinhController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Maloi,Malophoc,Magiaovien,Tenloi,Motaloi,Ngayvipham")] Loiquytrinh loiquytrinh)
        {
            if (id != loiquytrinh.Maloi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loiquytrinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoiquytrinhExists(loiquytrinh.Maloi))
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
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                           .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                           "Magiaovien", "HoTen", loiquytrinh.Magiaovien);

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", loiquytrinh.Malophoc);
            return View(loiquytrinh);
        }

        // GET: LoiQuyTrinhController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loiquytrinh = await _context.Loiquytrinhs
                .Include(d => d.MagiaovienNavigation)
                .Include(d => d.MalophocNavigation)
                .FirstOrDefaultAsync(m => m.Maloi == id);
            if (loiquytrinh == null)
            {
                return NotFound();
            }

            return View(loiquytrinh);
        }

        // POST: LoiQuyTrinhController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loiquytrinh = await _context.Loiquytrinhs.FindAsync(id);
            if (loiquytrinh != null)
            {
                _context.Loiquytrinhs.Remove(loiquytrinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoiquytrinhExists(int id)
        {
            return _context.Loiquytrinhs.Any(e => e.Maloi == id);
        }
    }
}
