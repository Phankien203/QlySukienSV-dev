using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class ThanhVienCLBController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ThanhVienCLBController(ApplicationDbContext ctx) { _context = ctx; }

        public async Task<IActionResult> Index()
        {
            var list = await _context.ThanhVienCLBs.Include(t => t.CLB).Include(t => t.NguoiDung).OrderByDescending(t => t.NgayThamGia).ToListAsync();
            return View(list);
        }



        public async Task<IActionResult> Duyet(int id)
        {
            var tv = await _context.ThanhVienCLBs.FindAsync(id);
            if (tv == null)
                return NotFound();
            tv.TrangThai = TrangThaiThanhVien.DaDuyet;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã duyệt.";


            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> TuChoi(int id)
        {
            var tv = await _context.ThanhVienCLBs.FindAsync(id);
            if (tv == null) return NotFound();
            tv.TrangThai = TrangThaiThanhVien.TuChoi;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã từ chối.";


            return RedirectToAction(nameof(Index));
        }
    }
}
