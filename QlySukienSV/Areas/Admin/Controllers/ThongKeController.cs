using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ThongKeController(ApplicationDbContext ctx) { _context = ctx; }
        public async Task<IActionResult> Index()
        {
            var clb = await _context.CLBs.CountAsync();
            var tv = await _context.ThanhVienCLBs.CountAsync();
            var sk = await _context.SuKiens.CountAsync();
            var dd = await _context.DiemDanhSuKiens.CountAsync();
            ViewBag.CLBDem = clb; ViewBag.ThanhVienDem = tv; ViewBag.SuKienDem = sk; ViewBag.DiemDanhDem = dd;
            return View();
        }
    }
}
