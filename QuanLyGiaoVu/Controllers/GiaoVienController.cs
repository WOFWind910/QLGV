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
    public class GiaoVienController : Controller
    {
        private readonly QlgvContext _context;

        public GiaoVienController(QlgvContext context)
        {
            _context = context;
        }

        // GET: GiaoVien
        public async Task<IActionResult> Index(string? searchString, int page=1, int pagesize=5)
        {
            var gv = _context.Giaoviens.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                gv = gv.Where(s => s.Tengv.Contains(searchString));
                ViewBag.SearchString = searchString;
            }
            return View(await gv.ToPagedListAsync(page, pagesize));
        }


        // GET: GiaoVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GiaoVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magiaovien,Hogv,Tengv,Ngaysinh,Gioitinh,Sodienthoai")] Giaovien giaovien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaovien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giaovien);
        }

        // GET: GiaoVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaovien = await _context.Giaoviens.FindAsync(id);
            if (giaovien == null)
            {
                return NotFound();
            }
            return View(giaovien);
        }

        // POST: GiaoVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magiaovien,Hogv,Tengv,Ngaysinh,Gioitinh,Sodienthoai")] Giaovien giaovien)
        {
            if (id != giaovien.Magiaovien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaovien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaovienExists(giaovien.Magiaovien))
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
            return View(giaovien);
        }

        // GET: GiaoVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaovien = await _context.Giaoviens
                .FirstOrDefaultAsync(m => m.Magiaovien == id);
            if (giaovien == null)
            {
                return NotFound();
            }

            return View(giaovien);
        }

        // POST: GiaoVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaovien = await _context.Giaoviens.FindAsync(id);
            if (giaovien != null)
            {
                _context.Giaoviens.Remove(giaovien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaovienExists(int id)
        {
            return _context.Giaoviens.Any(e => e.Magiaovien == id);
        }
    }
}
