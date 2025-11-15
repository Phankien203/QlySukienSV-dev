using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class DiemDanhController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DiemDanhController(ApplicationDbContext ctx) { _context = ctx; }
        public async Task<IActionResult> Index()
        {
            var list = await _context.DiemDanhSuKiens.Include(d => d.SuKien)!.ThenInclude(s => s!.CLB).Include(d => d.NguoiDung).OrderByDescending(d => d.ThoiGianDiemDanh).ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> ExportCsv()
        {
            var items = await _context.DiemDanhSuKiens
                .Include(d => d.SuKien)!.ThenInclude(s => s!.CLB)
                .Include(d => d.NguoiDung)
                .OrderBy(d => d.ThoiGianDiemDanh)
                .ToListAsync();
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("SuKien,CLB,SinhVien,MaSoSV,ThoiGian,HinhThuc");
            foreach (var x in items)
            {
                sb.AppendLine($"{x.SuKien?.TenSuKien},{x.SuKien?.CLB?.TenCLB},{x.NguoiDung?.HoTen},{x.NguoiDung?.MaSoSV},{x.ThoiGianDiemDanh:yyyy-MM-dd HH:mm},{x.HinhThuc}");
            }
            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "diemdanh.csv");
        }
    }
}

