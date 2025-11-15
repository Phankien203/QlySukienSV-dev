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
    public class ThanhVienCLBController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ThanhVienCLBController(ApplicationDbContext context) { _context = context; }

        public async Task<IActionResult> DanhSachCLB()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var list = await _context.ThanhVienCLBs
                .Include(t => t.CLB)
                .Where(t => t.NguoiDungId == userId)
                .OrderByDescending(t => t.NgayThamGia)
                .ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> DangKy(int clbId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var exists = await _context.ThanhVienCLBs.AnyAsync(t => t.CLBId == clbId && t.NguoiDungId == userId);
            if (!exists)
            {
                _context.ThanhVienCLBs.Add(new ThanhVienCLB
                {
                    CLBId = clbId,
                    NguoiDungId = userId,
                    TrangThai = TrangThaiThanhVien.ChoDuyet,
                    VaiTro = CLBRole.ThanhVien,
                    NgayThamGia = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã gửi yêu cầu tham gia CLB.";
            }
            else
            {
                TempData["Info"] = "Bạn đã là thành viên hoặc đã gửi yêu cầu.";
            }
            return RedirectToAction("ChiTietCLB", "Home", new { id = clbId });
        }
    }
}
