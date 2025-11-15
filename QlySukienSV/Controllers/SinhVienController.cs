using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlySukienSV.Data;
using QlySukienSV.ViewModels;
using System.Security.Claims;

namespace QlySukienSV.Controllers
{
    [Authorize]
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SinhVienController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> ThongTinCaNhan()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var sv = await _context.NguoiDungs.FindAsync(id);
            if (sv == null) return NotFound();
            var vm = new ProfileViewModel
            {
                Id = sv.Id,
                MaSoSV = sv.MaSoSV ?? string.Empty,
                HoTen = sv.HoTen,
                Lop = sv.Lop,
                Email = sv.Email,
                SoDienThoai = sv.SoDienThoai,
                VaiTro = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ThongTinCaNhan(ProfileViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var sv = await _context.NguoiDungs.FindAsync(id);
            if (sv == null)
                return NotFound();
            sv.HoTen = vm.HoTen; sv.Lop = vm.Lop; sv.Email = vm.Email; sv.SoDienThoai = vm.SoDienThoai;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật thành công";

            return RedirectToAction(nameof(ThongTinCaNhan));
        }
    }
}
