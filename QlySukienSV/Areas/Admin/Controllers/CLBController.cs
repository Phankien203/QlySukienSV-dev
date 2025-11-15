using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;
using QlySukienSV.Models;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class CLBController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CLBController(ApplicationDbContext ctx) { _context = ctx; }

        public async Task<IActionResult> Index()
        {
            var list = await _context.CLBs.Include(c => c.ChuNhiem).Include(c => c.ThanhViens).OrderBy(c => c.TenCLB).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.NguoiDungs = new SelectList(await _context.NguoiDungs.OrderBy(s => s.HoTen).ToListAsync(), "Id", "HoTen");
            return View(new CLB { NgayThanhLap = DateTime.Today });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CLB model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.NguoiDungs = new SelectList(await _context.NguoiDungs.OrderBy(s => s.HoTen).ToListAsync(), "Id", "HoTen");
                return View(model);
            }
            _context.CLBs.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã tạo CLB.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var clb = await _context.CLBs.FindAsync(id);
            if (clb == null)
                return NotFound();

            ViewBag.NguoiDungs = new SelectList(await _context.NguoiDungs.OrderBy(s => s.HoTen).ToListAsync(), "Id", "HoTen");


            return View(clb);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CLB model)
        {
            if (id != model.Id)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.NguoiDungs = new SelectList(await _context.NguoiDungs.OrderBy(s => s.HoTen).ToListAsync(), "Id", "HoTen");
                return View(model);
            }
            _context.Update(model); await _context.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật CLB.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clb = await _context.CLBs.FindAsync(id);
            if (clb == null)
                return NotFound();
            _context.CLBs.Remove(clb);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa CLB.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var clb = await _context.CLBs
                .Include(c => c.ThanhViens)
                    .ThenInclude(tv => tv.NguoiDung)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clb == null)
            {
                return NotFound();
            }

            return View(clb);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatVaiTro(int thanhVienId, string vaiTro)
        {
            var thanhVien = await _context.ThanhVienCLBs.FindAsync(thanhVienId);
            if (thanhVien == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thành viên!" });
            }

            thanhVien.VaiTro = vaiTro;
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Cập nhật vai trò thành công!" });
        }
    }
}
