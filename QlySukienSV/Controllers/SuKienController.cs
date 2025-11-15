using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;
using QlySukienSV.Models;
using System.Security.Claims;

namespace QlySukienSV.Controllers
{
    [Authorize]
    public class SuKienController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SuKienController(ApplicationDbContext context) { _context = context; }

        [AllowAnonymous]
        public async Task<IActionResult> DanhSachSuKien(int? clbId)
        {
            var query = _context.SuKiens.Include(s => s.CLB).AsQueryable();
            if (clbId.HasValue) query = query.Where(s => s.CLBId == clbId);
            var list = await query.OrderBy(s => s.NgayBatDau).ToListAsync();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> ThamGia(int suKienId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var exists = await _context.DangKySuKiens.AnyAsync(d => d.SuKienId == suKienId && d.NguoiDungId == userId);
            if (!exists)
            {
                _context.DangKySuKiens.Add(new DangKySuKien
                {
                    SuKienId = suKienId,
                    NguoiDungId = userId,
                    TrangThai = TrangThaiDangKy.ChoDuyet,
                    ThoiGianDangKy = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã đăng ký tham gia sự kiện.";
            }
            else
            {
                TempData["Info"] = "Bạn đã đăng ký sự kiện này.";
            }
            return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
        }

        public async Task<IActionResult> LichSuThamGia()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var list = await _context.DangKySuKiens
                .Include(d => d.SuKien)!.ThenInclude(s => s!.CLB)
                .Where(d => d.NguoiDungId == userId)
                .OrderByDescending(d => d.ThoiGianDangKy)
                .ToListAsync();
            return View(list);
        }
    }
}
