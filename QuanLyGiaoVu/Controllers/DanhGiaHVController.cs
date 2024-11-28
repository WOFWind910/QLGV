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
    public class DanhGiaHVController : Controller
    {
        private readonly QlgvContext _context;

        public DanhGiaHVController(QlgvContext context)
        {
            _context = context;
        }

        // GET: DanhGiaHV
        public async Task<IActionResult> Index(int page = 1, int pagesize = 5)
        {
            var qlgvContext = _context.Danhgiahocviens.Include(d => d.MagiaovienNavigation).Include(d => d.MahocvienNavigation).Include(d => d.MalophocNavigation).OrderByDescending(s=>s.Diemso);
            return View(await qlgvContext.ToPagedListAsync(page, pagesize));
        }


        // GET: DanhGiaHV/Create
        public IActionResult Create()
        {
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");

            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                "Mahocvien", "HoTen");

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View();
        }

        // POST: DanhGiaHV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stthv,Malophoc,Mahocvien,Magiaovien,Nhanxet,Diemso")] Danhgiahocvien danhgiahocvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhgiahocvien);
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
                "Magiaovien", "HoTen", danhgiahocvien.Magiaovien);

            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                "Mahocvien", "HoTen", danhgiahocvien.Mahocvien);

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", danhgiahocvien.Malophoc);
            return View(danhgiahocvien);
        }

        // GET: DanhGiaHV/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgiahocvien = await _context.Danhgiahocviens.FindAsync(id);
            if (danhgiahocvien == null)
            {
                return NotFound();
            }
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");

            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                "Mahocvien", "HoTen");

            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View(danhgiahocvien);
        }

        // POST: DanhGiaHV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Stthv,Malophoc,Mahocvien,Magiaovien,Nhanxet,Diemso")] Danhgiahocvien danhgiahocvien)
        {
            if (id != danhgiahocvien.Stthv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhgiahocvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhgiahocvienExists(danhgiahocvien.Stthv))
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
                           "Magiaovien", "HoTen", danhgiahocvien.Magiaovien);

            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                "Mahocvien", "HoTen", danhgiahocvien.Mahocvien);
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", danhgiahocvien.Malophoc);
            return View(danhgiahocvien);
        }

        // GET: DanhGiaHV/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgiahocvien = await _context.Danhgiahocviens
                .Include(d => d.MagiaovienNavigation)
                .Include(d => d.MahocvienNavigation)
                .Include(d => d.MalophocNavigation)
                .FirstOrDefaultAsync(m => m.Stthv == id);
            if (danhgiahocvien == null)
            {
                return NotFound();
            }

            return View(danhgiahocvien);
        }

        // POST: DanhGiaHV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhgiahocvien = await _context.Danhgiahocviens.FindAsync(id);
            if (danhgiahocvien != null)
            {
                _context.Danhgiahocviens.Remove(danhgiahocvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhgiahocvienExists(int id)
        {
            return _context.Danhgiahocviens.Any(e => e.Stthv == id);
        }
    }
}
