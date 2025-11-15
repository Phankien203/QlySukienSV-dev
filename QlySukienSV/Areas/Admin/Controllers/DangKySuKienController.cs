using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class DangKySuKienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangKySuKienController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.DangKySuKiens
                .Include(d => d.SuKien)
                .Include(d => d.NguoiDung)
                .Where(d => d.TrangThai == TrangThaiDangKy.ChoDuyet)
                .OrderBy(d => d.ThoiGianDangKy)
                .ToListAsync();

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Duyet(int id)
        {
            var dangKy = await _context.DangKySuKiens.FindAsync(id);
            if (dangKy == null)
            {
                return NotFound();
            }

            dangKy.TrangThai = TrangThaiDangKy.DaDuyet;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã duyệt yêu cầu tham gia sự kiện thành công!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> TuChoi(int id)
        {
            var dangKy = await _context.DangKySuKiens.FindAsync(id);
            if (dangKy == null)
            {
                return NotFound();
            }

            dangKy.TrangThai = TrangThaiDangKy.TuChoi;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã từ chối yêu cầu tham gia sự kiện!";
            return RedirectToAction(nameof(Index));
        }
    }
}
