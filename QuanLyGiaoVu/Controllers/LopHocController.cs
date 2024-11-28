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
    public class LopHocController : Controller
    {
        private readonly QlgvContext _context;

        public LopHocController(QlgvContext context)
        {
            _context = context;
        }

        // GET: LopHoc
        public async Task<IActionResult> Index(int page = 1, int pagesize = 5)
        {
            var qlgvContext = _context.Lophocs.Include(l => l.MagiaovienNavigation);
            return View(await qlgvContext.ToPagedListAsync(page, pagesize));
        }

        // GET: LopHoc/Create
        public IActionResult Create()
        {
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");
            return View();
        }

        // POST: LopHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Malophoc,Magiaovien,Tenlophoc,Ngaybatdau,Ngayketthuc,Lichhoc")] Lophoc lophoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lophoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen", lophoc.Magiaovien);
            return View(lophoc);
        }

        // GET: LopHoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophocs.FindAsync(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            ViewData["Magiaovien"] = new SelectList(_context.Giaoviens
                .Select(gv => new { gv.Magiaovien, HoTen = gv.Hogv + " " + gv.Tengv }),
                "Magiaovien", "HoTen");
            return View(lophoc);
        }

        // POST: LopHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Malophoc,Magiaovien,Tenlophoc,Ngaybatdau,Ngayketthuc,Lichhoc")] Lophoc lophoc)
        {
            if (id != lophoc.Malophoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lophoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LophocExists(lophoc.Malophoc))
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
                "Magiaovien", "HoTen", lophoc.Magiaovien);
            return View(lophoc);
        }

        // GET: LopHoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophocs
                .Include(l => l.MagiaovienNavigation)
                .FirstOrDefaultAsync(m => m.Malophoc == id);
            if (lophoc == null)
            {
                return NotFound();
            }

            return View(lophoc);
        }

        // POST: LopHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lophoc = await _context.Lophocs.FindAsync(id);
            if (lophoc != null)
            {
                _context.Lophocs.Remove(lophoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LophocExists(int id)
        {
            return _context.Lophocs.Any(e => e.Malophoc == id);
        }
    }
}
