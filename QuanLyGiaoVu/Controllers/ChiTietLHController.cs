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
    public class ChiTietLHController : Controller
    {
        private readonly QlgvContext _context;

        public ChiTietLHController(QlgvContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ChiTietLH(int? id = null, int page = 1, int pagesize = 5)
        {
            var ctlp = _context.Thongtinchitietlophocs.Include(lp => lp.MalophocNavigation).Include(hv => hv.MahocvienNavigation).AsQueryable();
            int? malophocId = TempData["MalophocId"] as int?;
            if (id != null)
            {
                ctlp = ctlp.Where(s => s.Malophoc == id);
            }
            else
            {
                ctlp = ctlp.Where(s => s.Malophoc > 0);
            }
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", id);
            return View(await ctlp.ToPagedListAsync(page, pagesize));
        }
        public IActionResult ThemHV()
        {
            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                "Mahocvien", "HoTen");
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemHV(Thongtinchitietlophoc thongtinchitietlophoc)
        {
            if (ModelState.IsValid)
            {
                bool hocVienDaTonTai = await _context.Thongtinchitietlophocs.AnyAsync(hv => hv.Mahocvien == thongtinchitietlophoc.Mahocvien && hv.Malophoc == thongtinchitietlophoc.Malophoc);

                if (hocVienDaTonTai)
                {
                    // Học viên đã tồn tại trong lớp, hiển thị thông báo lỗi
                    ModelState.AddModelError("", "Học viên này đã tồn tại trong lớp.");
                    TempData["MalophocId"] = thongtinchitietlophoc.Malophoc;
                }
                else
                {
                    // Học viên chưa tồn tại trong lớp, thêm vào cơ sở dữ liệu
                    _context.Add(thongtinchitietlophoc);
                    await _context.SaveChangesAsync();
                    TempData["MalophocId"] = thongtinchitietlophoc.Malophoc;
                    return RedirectToAction(nameof(ChiTietLH));
                }
            }
            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                            .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                            "Mahocvien", "HoTen", thongtinchitietlophoc.Mahocvien);
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", thongtinchitietlophoc.Malophoc);
            return View(thongtinchitietlophoc);
        }


        // GET: Thongtinchitietlophocs/Edit/5
        public async Task<IActionResult> SuaHV(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongtinchitietlophoc = await _context.Thongtinchitietlophocs.FindAsync(id);
            if (thongtinchitietlophoc == null)
            {
                return NotFound();
            }
            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
              .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
              "Mahocvien", "HoTen");
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc");
            return View(thongtinchitietlophoc);
        }

        // POST: Thongtinchitietlophocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        private bool ThongtinchitietlophocExists(int id)
        {
            return _context.Thongtinchitietlophocs.Any(e => e.Stt == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaHV(int id, Thongtinchitietlophoc thongtinchitietlophoc)
        {
            if (id != thongtinchitietlophoc.Stt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool hocVienDaTonTai = await _context.Thongtinchitietlophocs.AnyAsync(hv => hv.Mahocvien == thongtinchitietlophoc.Mahocvien && hv.Malophoc == thongtinchitietlophoc.Malophoc);

                    if (hocVienDaTonTai)
                    {
                        // Học viên này đã tồn tại trong lớp, hiển thị thông báo lỗi và quay lại view `SuaHV`
                        ModelState.AddModelError("", "Học viên này đã tồn tại trong lớp.");

                        // Tải lại dữ liệu dropdown
                        ViewData["Mahocvien"] = new SelectList(_context.Hocviens
                            .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
                            "Mahocvien", "HoTen", thongtinchitietlophoc.Mahocvien);
                        ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", thongtinchitietlophoc.Malophoc);

                        // Quay lại view `SuaHV` để hiển thị lỗi
                        return View(thongtinchitietlophoc);
                    }
                    else
                    {
                        // Không có trùng lặp, thực hiện cập nhật thông tin
                        _context.Update(thongtinchitietlophoc);
                        await _context.SaveChangesAsync();
                        TempData["MalophocId"] = thongtinchitietlophoc.Malophoc;
                        return RedirectToAction(nameof(ChiTietLH));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongtinchitietlophocExists(thongtinchitietlophoc.Stt))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["Mahocvien"] = new SelectList(_context.Hocviens
              .Select(hv => new { hv.Mahocvien, HoTen = hv.Hohv + " " + hv.Tenhv }),
              "Mahocvien", "HoTen", thongtinchitietlophoc.Mahocvien);
            ViewData["Malophoc"] = new SelectList(_context.Lophocs, "Malophoc", "Tenlophoc", thongtinchitietlophoc.Malophoc);
            return View(thongtinchitietlophoc);
        }

        // POST: Thongtinchitietlophocs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaHv()
        {
            var id = Convert.ToInt32(Request.Form["Stt"]);
            var thongtinchitietlophoc = await _context.Thongtinchitietlophocs.FindAsync(id);
            if (thongtinchitietlophoc != null)
            {
                _context.Thongtinchitietlophocs.Remove(thongtinchitietlophoc);
            }
            await _context.SaveChangesAsync();
            TempData["MalophocId"] = thongtinchitietlophoc?.Malophoc;
            return RedirectToAction(nameof(ChiTietLH));
        }
    }
}
