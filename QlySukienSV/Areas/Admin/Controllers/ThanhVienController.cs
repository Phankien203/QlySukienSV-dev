using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class ThanhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThanhVienController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<IActionResult> Index(string? clbFilter, string? khoaFilter, string? lopFilter)
        {
            // Lấy danh sách tất cả thành viên trong CLB
            var query = _context.ThanhVienCLBs
                .Include(tv => tv.NguoiDung)
                .Include(tv => tv.CLB)
                .AsQueryable();

            // Filter theo CLB
            if (!string.IsNullOrEmpty(clbFilter))
            {
                if (int.TryParse(clbFilter, out int clbId))
                {
                    query = query.Where(tv => tv.CLBId == clbId);
                }
            }

            // Filter theo Khóa (extract từ Lop, ví dụ: "CNTT K45" -> "K45")
            if (!string.IsNullOrEmpty(khoaFilter))
            {
                query = query.Where(tv => tv.NguoiDung != null && tv.NguoiDung.Lop != null && tv.NguoiDung.Lop.Contains(khoaFilter));
            }

            // Filter theo Lớp
            if (!string.IsNullOrEmpty(lopFilter))
            {
                query = query.Where(tv => tv.NguoiDung != null && tv.NguoiDung.Lop == lopFilter);
            }

            var list = await query.OrderBy(tv => tv.CLB!.TenCLB).ThenBy(tv => tv.NguoiDung!.HoTen).ToListAsync();

            // Lấy danh sách CLB để filter
            ViewBag.CLBs = await _context.CLBs.OrderBy(c => c.TenCLB).ToListAsync();

            // Lấy danh sách Khóa unique (extract từ Lop)
            var allLops = await _context.NguoiDungs
                .Where(n => n.Lop != null)
                .Select(n => n.Lop)
                .Distinct()
                .ToListAsync();

            var khoas = allLops
                .Where(l => l != null && l.Contains("K"))
                .Select(l =>
                {
                    var parts = l!.Split(' ');
                    return parts.FirstOrDefault(p => p.StartsWith("K"));
                })
                .Where(k => k != null)
                .Distinct()
                .OrderByDescending(k => k)
                .ToList();

            ViewBag.Khoas = khoas;
            ViewBag.Lops = allLops.Where(l => l != null).OrderBy(l => l).ToList();

            ViewBag.CurrentCLBFilter = clbFilter;
            ViewBag.CurrentKhoaFilter = khoaFilter;
            ViewBag.CurrentLopFilter = lopFilter;

            return View(list);
        }
    }
}
