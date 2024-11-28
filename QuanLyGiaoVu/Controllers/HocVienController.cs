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
    public class HocVienController : Controller
    {
        private readonly QlgvContext _context;

        public HocVienController(QlgvContext context)
        {
            _context = context;
        }

        // GET: HocVien
        public async Task<IActionResult> Index(string? searchString, int page = 1, int pagesize = 5)
        {
            var hv = _context.Hocviens.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                hv = hv.Where(s => s.Tenhv.Contains(searchString));
                ViewBag.SearchString = searchString;
            }
            var pagelist = await hv.ToPagedListAsync(page, pagesize);
            return View(pagelist);
        }

        //// GET: HocVien/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var hocvien = await _context.Hocviens
        //        .FirstOrDefaultAsync(m => m.Mahocvien == id);
        //    if (hocvien == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(hocvien);
        //}

        // GET: HocVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HocVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahocvien,Hohv,Tenhv,Ngaysinh,Gioitinh,Sodienthoaiphuhuynh")] Hocvien hocvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(hocvien);
        }

        // GET: HocVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocvien = await _context.Hocviens.FindAsync(id);
            if (hocvien == null)
            {
                return NotFound();
            }
            return View(hocvien);
        }

        // POST: HocVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mahocvien,Hohv,Tenhv,Ngaysinh,Gioitinh,Sodienthoaiphuhuynh")] Hocvien hocvien)
        {
            if (id != hocvien.Mahocvien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocvienExists(hocvien.Mahocvien))
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
            return View(hocvien);
        }

        // GET: HocVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocvien = await _context.Hocviens
                .FirstOrDefaultAsync(m => m.Mahocvien == id);
            if (hocvien == null)
            {
                return NotFound();
            }

            return View(hocvien);
        }

        // POST: HocVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hocvien = await _context.Hocviens.FindAsync(id);
            if (hocvien != null)
            {
                _context.Hocviens.Remove(hocvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocvienExists(int id)
        {
            return _context.Hocviens.Any(e => e.Mahocvien == id);
        }
    }
}
