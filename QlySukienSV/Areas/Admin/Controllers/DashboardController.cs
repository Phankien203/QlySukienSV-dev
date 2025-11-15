using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext ctx) { _context = ctx; }
        public async Task<IActionResult> Index()
        {
            // Thống kê tổng quan
            ViewBag.CLBDem = await _context.CLBs.Where(clb => clb.TrangThai == TrangThaiCLB.Active).CountAsync();
            ViewBag.SVDem = await _context.NguoiDungs.CountAsync();
            ViewBag.SuKienDem = await _context.SuKiens.CountAsync();
            ViewBag.ThanhVienDem = await _context.ThanhVienCLBs.Where(mem => mem.TrangThai == TrangThaiThanhVien.DaDuyet).CountAsync();

            // Thống kê CLB theo trạng thái
            ViewBag.CLBActive = await _context.CLBs.CountAsync(c => c.TrangThai == "Active");
            ViewBag.CLBInactive = await _context.CLBs.CountAsync(c => c.TrangThai == "Inactive");

            // Thống kê sự kiện theo trạng thái
            ViewBag.SuKienSapDienRa = await _context.SuKiens.CountAsync(s => s.TrangThai == "Sắp diễn ra");
            ViewBag.SuKienDangDienRa = await _context.SuKiens.CountAsync(s => s.TrangThai == "Đang diễn ra");
            ViewBag.SuKienHoanThanh = await _context.SuKiens.CountAsync(s => s.TrangThai == "Hoàn thành");

            // Thống kê thành viên theo vai trò
            ViewBag.ChuNhiem = await _context.ThanhVienCLBs.CountAsync(t => t.VaiTro == CLBRole.ChuNhiem);
            ViewBag.PhoChuNhiem = await _context.ThanhVienCLBs.CountAsync(t => t.VaiTro == CLBRole.PhoChuNhiem);
            ViewBag.BanChapHanh = await _context.ThanhVienCLBs.CountAsync(t => t.VaiTro == CLBRole.BanChapHanh);
            ViewBag.ThanhVien = await _context.ThanhVienCLBs.CountAsync(t => t.VaiTro == CLBRole.ThanhVien);
            ViewBag.Khach = await _context.ThanhVienCLBs.CountAsync(t => t.VaiTro == CLBRole.Khach);

            ViewBag.SinhVienThamGiaCLB = await _context.ThanhVienCLBs
                .Where(tv => tv.TrangThai == TrangThaiThanhVien.DaDuyet)
                .CountAsync();

            return View();
        }
    }
}
