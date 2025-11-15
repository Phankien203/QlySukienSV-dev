using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;
using QlySukienSV.Models;
using QlySukienSV.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace QlySukienSV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                Clbs = await _context.CLBs
                    .Include(c => c.ThanhViens)
                    .OrderBy(c => c.TenCLB)
                    .ToListAsync(),

                NoiBatSuKiens = await _context.SuKiens
                    .Include(sk => sk.CLB)
                    .Where(sk => sk.NoiBat && sk.TrangThai == TrangThaiSuKien.SapDienRa)
                    .OrderBy(sk => sk.NgayBatDau)
                    .Take(3)
                    .ToListAsync(),

                UpcomingSuKiens = await _context.SuKiens
                    .Include(sk => sk.CLB)
                    .Where(sk => sk.NgayBatDau >= DateTime.Today)
                    .OrderBy(sk => sk.NgayBatDau)
                    .Take(5)
                    .ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> ChiTietCLB(int id)
        {
            var club = await _context.CLBs
                .Include(c => c.ChuNhiem)
                .Include(c => c.ThanhViens)
                    .ThenInclude(tv => tv.NguoiDung)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
            {
                return NotFound();
            }

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var membership = club.ThanhViens.FirstOrDefault(tv => tv.NguoiDungId == userId);
                ViewBag.CurrentMembership = membership;
            }

            return View(club);
        }

        public async Task<IActionResult> ChiTietSuKien(int id)
        {
            var suKien = await _context.SuKiens
                .Include(sk => sk.CLB)
                .Include(sk => sk.DangKySuKiens)
                    .ThenInclude(dk => dk.NguoiDung)
                .FirstOrDefaultAsync(sk => sk.Id == id);

            if (suKien == null)
            {
                return NotFound();
            }

            // Initialize ViewBag properties with default values for all users
            ViewBag.CoTheDiemDanh = false;
            ViewBag.DaDiemDanh = false;

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var registration = suKien.DangKySuKiens.FirstOrDefault(dk => dk.NguoiDungId == userId);

                ViewBag.CurrentRegistration = registration;

                // Kiểm tra thời gian điểm danh
                var vietnamTime = DateTime.UtcNow.AddHours(7);
                var thoiGianChoPhepBatDau = suKien.NgayBatDau.AddHours(-2);
                var thoiGianKetThuc = suKien.NgayKetThuc;

                // Cho phép điểm danh nếu trong khoảng thời gian hợp lệ
                ViewBag.CoTheDiemDanh = vietnamTime >= thoiGianChoPhepBatDau && vietnamTime <= thoiGianKetThuc;

                // Kiểm tra đã điểm danh chưa
                var daDiemDanh = await _context.DiemDanhSuKiens
                    .AnyAsync(dd => dd.SuKienId == id && dd.NguoiDungId == userId);
                ViewBag.DaDiemDanh = daDiemDanh;
            }

            return View(suKien);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
